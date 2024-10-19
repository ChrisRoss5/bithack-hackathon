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
}