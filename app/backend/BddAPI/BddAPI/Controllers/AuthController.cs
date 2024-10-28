using BddAPI.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ITokenService tokenService) : ControllerBase
{
    [HttpPost("authorize-firebase-client")]
    public async Task<IActionResult> FirebaseLogin(string firebaseToken)
    {
        var tokens = await tokenService.GenerateAccessTokenWithFirebase(firebaseToken);
        return Ok(tokens);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var tokens = await tokenService.RefreshAccessToken(refreshToken);
        return Ok(tokens);
    }
}