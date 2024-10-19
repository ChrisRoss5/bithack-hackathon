using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Models;
using BddAPI.Repositories;

namespace BddAPI.Services;

public interface IContractService
{
    Task<ContractResponseDto> CreateContract(ContractRangeRequestDto contractRangeRequestDtos);
}

public class ContractService(IContractRepository contractRepository, IMapper mapper) : IContractService
{
    public async Task<ContractResponseDto> CreateContract(ContractRangeRequestDto contractWithRangesDto)
    {
        // Extract the individual components from the DTO
        var contractRequestDto = contractWithRangesDto.ContractRequest;
        var dateRange = contractWithRangesDto.ContractRanges;

        // Map the ContractRequestDto to a Contract entity
        var contract = mapper.Map<Contract>(contractRequestDto);

        // Create the contract in the repository
        var storedContract = await contractRepository.CreateContract(contract);

        // Map and assign the date ranges to the contract
        var contractRanges = mapper.Map<List<ContractRange>>(dateRange);
        await contractRepository.AssignContractDateRanges(contractRanges, storedContract.Id);

        // Save the changes
        await contractRepository.SaveChangesAsync();

        // Return the response DTO
        return mapper.Map<ContractResponseDto>(contract);
    }
}