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

    Task<UserResponseDto> UpdateUserAsync(string firebaseUid, UserRequestDto userRequestDto);
    Task DeleteUserAsync(string firebaseUid);
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

    public async Task<UserResponseDto> UpdateUserAsync(string firebaseUid, UserRequestDto userRequestDto)
    {
        var user = await userRepository.GetUserByFirebaseUid(firebaseUid);

        if (user == null)
            throw new NotFoundException("User not found");

        mapper.Map(userRequestDto, user);
        await userRepository.UpdateUser(user);

        return mapper.Map<UserResponseDto>(user);
    }

    public async Task DeleteUserAsync(string firebaseUid)
    {
        var existingUser = await userRepository.GetUserByFirebaseUid(firebaseUid);

        if (existingUser == null)
            throw new NotFoundException("User not found");

        await userRepository.DeleteUser(existingUser);
    }
}