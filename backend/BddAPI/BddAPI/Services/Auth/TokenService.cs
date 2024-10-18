using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BddAPI.Data;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BddAPI.Services.Auth;

public interface ITokenService
{
    Task<TokensResponse> GenerateAccessTokenWithFirebase(string firebaseToken);
    Task<TokensResponse> RefreshAccessToken(string refreshToken);
}

public class TokenService(
    IConfiguration configuration,
    IUserRepository userRepository,
    FirebaseAuth firebaseAuth,
    IUserService userService,
    IMapper mapper,
    BddDbContext dbContext)
    : ITokenService
{
    public async Task<TokensResponse> GenerateAccessTokenWithFirebase(string firebaseToken)
    {
        var decodedToken = await firebaseAuth.VerifyIdTokenAsync(firebaseToken);

        var userId = decodedToken.Claims["user_id"].ToString();
        var userEmail = decodedToken.Claims["email"].ToString();
        var username = decodedToken.Claims["name"].ToString();
        if (userId == null || userEmail == null || username == null)
        {
            throw new UserException("User ID, email or username not found in token");
        }

        var bddUser = await userRepository.GetUserByFirebaseUid(userId);
        if (bddUser == null)
        {
            var newBddUser = new User
            {
                FirebaseUid = userId,
                Email = userEmail,
                Username = username
            };
            bddUser = await userRepository.CreateUserAsync(newBddUser);
            var newBddUserReqDto = mapper.Map<UserRequestDto>(newBddUser);
            await userRepository.SaveChangesAsync();

            await userService.AssignDefaultRoleAsync(newBddUserReqDto);
            await userRepository.SaveChangesAsync();
        }

        var accessToken = await GenerateAccessToken(bddUser);
        var refreshToken = GenerateRefreshToken();
        await UpdateRefreshToken(bddUser.Id, refreshToken);

        return new TokensResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private async Task UpdateRefreshToken(Guid userId, string newRefreshToken)
    {
        var existingRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);

        if (existingRefreshToken != null)
        {
            existingRefreshToken.Token = newRefreshToken;
            existingRefreshToken.Expires = DateTime.Now.AddDays(7);
        }
        else
        {
            dbContext.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                Expires = DateTime.Now.AddDays(7),
                UserId = userId
            });
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<TokensResponse> RefreshAccessToken(string refreshToken)
    {
        var validRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken) ??
                                throw new NotFoundException("Refresh token not found");

        var bddUser = await userRepository.GetUserByRefreshToken(refreshToken);
        if (bddUser == null)
            throw new NotFoundException("User with this refresh token not found");

        if (validRefreshToken.Expires < DateTime.Now)
            throw new UnauthorizedException("Refresh token expired");

        var accessToken = await GenerateAccessToken(bddUser);

        return new TokensResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private async Task<string> GenerateAccessToken(User bddUser)
    {
        var jwtSettings = configuration.GetSection("JWT");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var userRoles = await userRepository.GetUserRoles(bddUser.Id);

        var userClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, bddUser.Id.ToString()),
            new(ClaimTypes.Name, bddUser.Username),
            new(ClaimTypes.Email, bddUser.Email)
        };

        userClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var accessToken = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }

    private static string GenerateRefreshToken()
    {
        const int tokenLengthInBytes = 64;
        var randomBytes = new byte[tokenLengthInBytes];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var refreshToken = Convert.ToBase64String(randomBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

        return refreshToken;
    }
}