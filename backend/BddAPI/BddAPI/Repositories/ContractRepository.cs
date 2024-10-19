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
        var contractRanges = await dbContext.ContractRanges
            .Where(cr => cr.From >= from && cr.To <= to)
            .ToListAsync();

        var contracts = await dbContext.Contracts.Where(c => contractRanges.Any(cr => cr.ContractId == c.Id))
            .ToListAsync();

        var communityHomes = await dbContext.CommunityHomes.ToListAsync();

        List<CommunityHomeWithContracts> communityHomeWithContracts = [];

        foreach (var communityHome in communityHomes)
        {
            var contractsForSearch = contracts
                .Where(c => c.CommunityHomeId == communityHome.Id)
                .Select(c => new ContractForSearch
                {
                    CommunityHomeId = c.CommunityHomeId,
                    CommunityHome = communityHome,
                    ContractRanges = contractRanges.Where(cr => cr.ContractId == c.Id).ToList()
                })
                .ToList();

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