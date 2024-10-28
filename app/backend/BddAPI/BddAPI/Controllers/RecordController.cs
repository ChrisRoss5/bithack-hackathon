using BddAPI.DTOs.Request;
using BddAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/record")]
public class RecordController(IRecordService recordService) : ControllerBase
{
    [HttpPost("create-record")]
    public async Task<IActionResult> CreateRecord(RecordRequestDto recordRequestDto)
    {
        var record = await recordService.CreateRecordAsync(recordRequestDto);
        return Ok(record);
    }

    [HttpGet("get-record")]
    public async Task<IActionResult> GetRecordById(Guid recordId)
    {
        var record = await recordService.GetRecordByIdAsync(recordId);
        return Ok(record);
    }

    [HttpGet("get-records")]
    public async Task<IActionResult> GetRecords()
    {
        var records = await recordService.GetRecordsAsync();
        return Ok(records);
    }

    [HttpGet("get-user-records")]
    public async Task<IActionResult> GetUserRecords(Guid userId)
    {
        var records = await recordService.GetUserRecordsAsync(userId);
        return Ok(records);
    }

    [HttpPut("update-record")]
    public async Task<IActionResult> UpdateRecord(Guid recordId, RecordRequestDto recordRequestDto)
    {
        var record = await recordService.UpdateRecordAsync(recordId, recordRequestDto);
        return Ok(record);
    }

    [HttpDelete("delete-record")]
    public async Task<IActionResult> DeleteRecord(Guid recordId)
    {
        await recordService.DeleteRecordAsync(recordId);
        return Ok();
    }
}