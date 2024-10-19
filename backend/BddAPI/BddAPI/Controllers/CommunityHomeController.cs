using BddAPI.DTOs.Request;
using BddAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/community-homes")]
public class CommunityHomeController(ICommunityHomeService communityHomeService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateCommunityCenterAsync(CommunityHomeRequestDto communityHomeRequestDto)
    {
        var communityCenter = await communityHomeService.CreateCommunityCenterAsync(communityHomeRequestDto);
        return Ok(communityCenter);
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetCommunityCenterAsync(Guid id)
    {
        var communityCenter = await communityHomeService.GetCommunityCenterAsync(id);
        return Ok(communityCenter);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetCommunityCentersAsync(int quantity)
    {
        var communityCenters = await communityHomeService.GetCommunityCentersAsync(quantity);
        return Ok(communityCenters);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCommunityCenterAsync(CommunityHomeRequestDto communityHomeRequestDto,
        Guid id)
    {
        var communityCenter = await communityHomeService.UpdateCommunityCenterAsync(communityHomeRequestDto, id);
        return Ok(communityCenter);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCommunityCenterAsync(Guid id)
    {
        await communityHomeService.DeleteCommunityCenterAsync(id);
        return Ok();
    }

    [HttpGet("get-by-availability")]
    public async Task<IActionResult> GetCommunityCentersByAvailabilityAsync(DateTime from, DateTime to)
    {
        var communityCenters = await communityHomeService.SearchCommunityHomesWithContractsAsync(from, to);
        return Ok(communityCenters);
    }
}