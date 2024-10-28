using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Enum;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;

namespace BddAPI.Services;

public interface IUserService
{
    Task<RoleType> AssignDefaultRoleAsync(UserRequestDto userRequestDto);
    Task<UserResponseDto> GetUserByFirebaseUid(string firebaseUid);
    Task<UserResponseDto> UpdateUserAsync(Guid userId, UserRequestDto userRequestDto);
    Task<UserResponseDto> GetUserById(Guid userId);
    Task AssignRoleToUserAsync(RoleType roleType, Guid userId);
    Task DeleteUserAsync(Guid userId);
}

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
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
        await userRepository.SaveChangesAsync();

        return RoleType.User;
    }

    public async Task<UserResponseDto> GetUserByFirebaseUid(string firebaseUid)
    {
        var user = await userRepository.GetUserByFirebaseUid(firebaseUid);

        if (user == null)
            throw new NotFoundException("User not found");

        var mappedUser = mapper.Map<UserResponseDto>(user);
        return mappedUser;
    }

    public async Task<UserResponseDto> UpdateUserAsync(Guid userId, UserRequestDto userRequestDto)
    {
        var user = await userRepository.GetUserById(userId);

        if (user == null)
            throw new NotFoundException("User not found");

        mapper.Map(userRequestDto, user);
        await userRepository.UpdateUser(user);

        return mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> GetUserById(Guid userId)
    {
        var user = await userRepository.GetUserById(userId);

        if (user == null)
            throw new NotFoundException("User not found");

        return mapper.Map<UserResponseDto>(user);
    }

    public async Task AssignRoleToUserAsync(RoleType roleType, Guid userId)
    {
        var user = await userRepository.GetUserById(userId);
        if (user == null)
            throw new UserException("User not found");

        var role = await userRepository.GetRoleByName(roleType.ToString());
        await userRepository.AssignRoleToUser(new UserRole
        {
            RoleId = role.Id,
            UserId = user.Id
        });

        await userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var existingUser = await userRepository.GetUserById(userId);

        if (existingUser == null)
            throw new NotFoundException("User not found");

        await userRepository.DeleteUser(existingUser);
    }
}