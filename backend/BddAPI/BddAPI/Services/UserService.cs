using BddAPI.DTOs.Request;
using BddAPI.Enum;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;

namespace BddAPI.Services;

public interface IUserService
{
    Task<RoleType> AssignDefaultRoleAsync(UserRequestDto userRequestDto);
}

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<RoleType> AssignDefaultRoleAsync(UserRequestDto userRequestDto)
    {
        if (userRequestDto.FirebaseUid == null)
            throw new UserException("Firebase UID not found in request");

        var user = await userRepository.GetUserByFirebaseUid(userRequestDto.FirebaseUid);
        var defaultRole = await userRepository.GetDefaultRole();
        if (user == null)
            throw new UserException("User not found");

        await userRepository.AssignRoleToUser(new UserRole
        {
            RoleId = defaultRole.Id,
            UserId = user.Id
        });

        await userRepository.SaveChangesAsync();

        return RoleType.User;
    }
}