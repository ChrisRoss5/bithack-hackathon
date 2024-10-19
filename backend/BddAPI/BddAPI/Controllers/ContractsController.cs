using BddAPI.DTOs.Request;
using BddAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractsController(IContractService contractService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateContract(ContractRangeRequestDto contractRequestDto)
    {
        var contract = await contractService.CreateContract(contractRequestDto);
        return Ok(contract);
    }

    [HttpPut("set-to-mayor-signed")]
    public async Task<IActionResult> SetContractToMayorSigned(Guid contractId)
    {
        var contract = await contractService.SetContractToMayorSignedStatus(contractId);
        return Ok(contract);
    }

    [HttpPut("set-to-prepared")]
    public async Task<IActionResult> SetContractToPrepared(Guid contractId)
    {
        var contract = await contractService.SetContractToPreparedStatus(contractId);
        return Ok(contract);
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetContractById(Guid contractId)
    {
        var contract = await contractService.GetByContractId(contractId);
        return Ok(contract);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteContract(Guid contractId)
    {
        await contractService.DeleteContract(contractId);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateContract(Guid contractId, ContractRequestDto contractRequestDto)
    {
        var contract = await contractService.UpdateContract(contractId, contractRequestDto);
        return Ok(contract);
    }
}