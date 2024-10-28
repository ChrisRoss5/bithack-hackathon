using BddAPI.DTOs.Request;
using BddAPI.Enum;
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

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStaus(Guid contractId, ContractStatus statsType)
    {
        var contract = await contractService.UpdateStatus(contractId, statsType);
        return Ok(contract);
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetContractById(Guid contractId)
    {
        var contract = await contractService.GetByContractId(contractId);
        return Ok(contract);
    }

    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetContractsByUserId(Guid userId)
    {
        var contracts = await contractService.GetContractsByUserId(userId);
        return Ok(contracts);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllContracts()
    {
        var contracts = await contractService.GetAllContracts();
        return Ok(contracts);
    }

    [HttpGet("get-contract-ranges")]
    public async Task<IActionResult> GetContractRangesByContractId(Guid contractId)
    {
        var contractRanges = await contractService.GetContractRangesByContractId(contractId);
        return Ok(contractRanges);
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