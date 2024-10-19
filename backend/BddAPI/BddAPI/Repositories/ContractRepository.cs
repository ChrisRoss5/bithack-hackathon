using BddAPI.Data;
using BddAPI.DTOs.Response;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Repositories;

public interface IContractRepository
{
    Task<Contract> CreateContract(Contract contract);
    Task AssignContractDateRanges(List<ContractRange> contractRanges, Guid contractId);
    Task SaveChangesAsync();
    Task<List<CommunityHomeWithContracts>> GetContractsForSearch(DateTime from, DateTime to);
}

public class ContractRepository(BddDbContext dbContext) : IContractRepository
{
    public async Task<Contract> CreateContract(Contract contract)
    {
        await dbContext.Contracts.AddAsync(contract);
        return contract;
    }

    public async Task AssignContractDateRanges(List<ContractRange> contractRanges, Guid contractId)
    {
        foreach (var contractRange in contractRanges)
        {
            contractRange.ContractId = contractId;
            await dbContext.ContractRanges.AddAsync(contractRange);
        }
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<CommunityHomeWithContracts>> GetContractsForSearch(DateTime from, DateTime to)
    {
        // Get the contract range ContractIds that fall within the date range
        var contractRangeIds = await dbContext.ContractRanges
            .Where(cr => cr.From >= from && cr.To <= to)
            .Select(cr => cr.ContractId) // We only need the ContractId at this stage
            .ToListAsync();

        // Use Contains to filter contracts that match the contractRangeIds
        var contracts = await dbContext.Contracts
            .Where(c => contractRangeIds.Contains(c.Id)) // This is the critical Contains
            .ToListAsync();

        // Fetch all community homes
        var communityHomes = await dbContext.CommunityHomes.ToListAsync();

        // Initialize the result list
        var communityHomeWithContracts = new List<CommunityHomeWithContracts>();

        // For each community home, find its related contracts and contract ranges
        foreach (var communityHome in communityHomes)
        {
            // Find contracts for this specific community home
            var contractsForSearch = contracts
                .Where(c => c.CommunityHomeId == communityHome.Id)
                .Select(c => new ContractForSearch
                {
                    CommunityHomeId = c.CommunityHomeId,
                    CommunityHome = communityHome,
                    ContractRanges =
                        dbContext.ContractRanges.Where(cr => cr.ContractId == c.Id).ToList() // Load related ranges
                })
                .ToList();

            // Add to the result list
            communityHomeWithContracts.Add(new CommunityHomeWithContracts
            {
                Id = communityHome.Id,
                Name = communityHome.Name,
                RentPrice = communityHome.RentPrice,
                Address = communityHome.Address,
                LeaseAmount = communityHome.LeaseAmount,
                BailAmount = communityHome.BailAmount,
                HomeBills = communityHome.HomeBills,
                Vat = communityHome.Vat,
                Area = communityHome.Area,
                CutleryPrice = communityHome.CutleryPrice,
                Capacity = communityHome.Capacity,
                CreatedAt = communityHome.CreatedAt,
                Contracts = contractsForSearch
            });
        }

        return communityHomeWithContracts;
    }
}