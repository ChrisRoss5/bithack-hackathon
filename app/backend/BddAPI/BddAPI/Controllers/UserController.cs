using BddAPI.DTOs.Request;
using BddAPI.Enum;
using BddAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("get-by-firebase-uid")]
    public async Task<IActionResult> GetUserByFirebaseUid(string firebaseUid)
    {
        return Ok(await userService.GetUserByFirebaseUid(firebaseUid));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(Guid userId, UserRequestDto userRequestDto)
    {
        return Ok(await userService.UpdateUserAsync(userId, userRequestDto));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await userService.DeleteUserAsync(userId);
        return Ok();
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        return Ok(await userService.GetUserById(userId));
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRoleToUser(RoleType roleType, Guid userId)
    {
        await userService.AssignRoleToUserAsync(roleType, userId);
        return Ok();
    }
}