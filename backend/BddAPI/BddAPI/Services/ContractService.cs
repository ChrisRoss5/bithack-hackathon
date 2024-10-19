using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Enum;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;

namespace BddAPI.Services;

public interface IContractService
{
    Task<ContractResponseDto> CreateContract(ContractRangeRequestDto contractRangeRequestDtos);
    Task<ContractResponseDto> SetContractToPreparedStatus(Guid contractId);
    Task<ContractResponseDto> SetContractToMayorSignedStatus(Guid contractId);
    Task<ContractResponseDto> GetByContractId(Guid contractId);
    Task<List<ContractResponseDto>> GetContractsByUserId(Guid userId);
    Task<List<ContractResponseDto>> GetAllContracts();
    Task DeleteContract(Guid contractId);
    Task<ContractResponseDto> UpdateContract(Guid contractId, ContractRequestDto contractRequestDto);
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

    public async Task<ContractResponseDto> SetContractToPreparedStatus(Guid contractId)
    {
        var updatedContract = await contractRepository.UpdateStatus(contractId, ContractStatus.Prepared);
        return mapper.Map<ContractResponseDto>(updatedContract);
    }

    public async Task<ContractResponseDto> SetContractToMayorSignedStatus(Guid contractId)
    {
        var updatedContract = await contractRepository.UpdateStatus(contractId, ContractStatus.MayorSigned);

        return mapper.Map<ContractResponseDto>(updatedContract);
    }

    public async Task<ContractResponseDto> GetByContractId(Guid contractId)
    {
        var retrievedContract = await contractRepository.GetContractById(contractId);

        if (retrievedContract == null)
        {
            throw new NotFoundException($"Contract with ID {contractId} not found");
        }

        return mapper.Map<ContractResponseDto>(retrievedContract);
    }

    public async Task<List<ContractResponseDto>> GetContractsByUserId(Guid userId)
    {
        var contracts = await contractRepository.GetContractsByUserId(userId);

        if (contracts == null)
        {
            throw new NotFoundException($"No contracts found for user with ID {userId}");
        }

        return mapper.Map<List<ContractResponseDto>>(contracts);
    }

    public async Task<List<ContractResponseDto>> GetAllContracts()
    {
        var contracts = await contractRepository.GetAllContracts();

        if (contracts == null)
        {
            throw new NotFoundException("No contracts found");
        }

        return mapper.Map<List<ContractResponseDto>>(contracts);
    }

    public async Task DeleteContract(Guid contractId)
    {
        var existingContract = await contractRepository.GetContractById(contractId);

        if (existingContract == null)
        {
            throw new NotFoundException($"Contract with ID {contractId} not found");
        }

        await contractRepository.DeleteContract(existingContract);
    }

    public async Task<ContractResponseDto> UpdateContract(Guid contractId, ContractRequestDto contractRequestDto)
    {
        var contract = await contractRepository.GetContractById(contractId);

        if (contract == null)
        {
            throw new NotFoundException($"Contract with ID {contractId} not found");
        }

        mapper.Map(contractRequestDto, contract);
        await contractRepository.UpdateContract(contract);

        return mapper.Map<ContractResponseDto>(contract);
    }
}