using BddAPI.Data;
using BddAPI.Exceptions;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByRefreshToken(string refreshToken);
    Task<User?> GetUserByFirebaseUid(string username);
    Task AssignRoleToUser(UserRole role);
    Task<IEnumerable<Role>> GetUserRoles(Guid userId);
    Task<User> UpdateUser(User user);
    Task<Role> GetDefaultRole();
    Task DeleteUser(User user);
    Task SaveChangesAsync();
}

public class UserRepository(BddDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        return await dbContext.RefreshTokens
            .Where(rt => rt.Token == refreshToken)
            .Select(rt => rt.User)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByFirebaseUid(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.FirebaseUid == username);
    }

    public async Task AssignRoleToUser(UserRole userRole)
    {
        await dbContext.UserRoles.AddAsync(userRole);
    }

    public async Task<IEnumerable<Role>> GetUserRoles(Guid userId)
    {
        return await dbContext.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<User> UpdateUser(User user)
    {
        dbContext.Users.Update(user);
        await SaveChangesAsync();
        return user;
    }

    public async Task<Role> GetDefaultRole()
    {
        return await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "User") ??
               throw new UserException("Default role not found");
    }

    public async Task DeleteUser(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}