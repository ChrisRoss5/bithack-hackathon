using BddAPI.DTOs.Request;
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
    public async Task<IActionResult> UpdateUser(string firebaseUid, UserRequestDto userRequestDto)
    {
        return Ok(await userService.UpdateUserAsync(firebaseUid, userRequestDto));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUser(string firebaseUid)
    {
        await userService.DeleteUserAsync(firebaseUid);
        return Ok();
    }
}