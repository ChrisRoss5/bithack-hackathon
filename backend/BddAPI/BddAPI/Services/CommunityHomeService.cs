using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BddAPI.Services;

public interface ICommunityHomeService
{
    Task<CommunityHomeResponseDto> CreateCommunityCenterAsync(CommunityHomeRequestDto communityCenterRequestDto);
    Task<CommunityHomeResponseDto> GetCommunityCenterAsync(Guid communityCenterId);
    Task<IEnumerable<CommunityHomeResponseDto>> GetCommunityCentersAsync(int quantity);

    Task<CommunityHomeResponseDto> UpdateCommunityCenterAsync(CommunityHomeRequestDto communityCenterRequestDto,
        Guid id);

    Task DeleteCommunityCenterAsync(Guid communityCenterId);

    Task<List<CommunityHomeWithContracts>> SearchCommunityHomesWithContractsAsync(DateTime from, DateTime to);
}

public class CommunityHomeService(
    ICommunityHomeRepository communityHomeRepository,
    IContractRepository contractRepository,
    IMapper mapper)
    : ICommunityHomeService
{
    public async Task<CommunityHomeResponseDto> CreateCommunityCenterAsync(
        CommunityHomeRequestDto communityCenterRequestDto)
    {
        var communityCenter = mapper.Map<CommunityHome>(communityCenterRequestDto);

        await communityHomeRepository.CreateCommunityCenterAsync(communityCenter);
        await communityHomeRepository.SaveChangesAsync();

        return mapper.Map<CommunityHomeResponseDto>(communityCenter);
    }

    public async Task<CommunityHomeResponseDto> GetCommunityCenterAsync(Guid communityCenterId)
    {
        var communityCenter = await communityHomeRepository.GetCommunityCenterAsync(communityCenterId);

        if (communityCenter == null)
        {
            throw new NotFoundException($"Community Center with ID: {communityCenterId} not found");
        }

        return mapper.Map<CommunityHomeResponseDto>(communityCenter);
    }

    public async Task<IEnumerable<CommunityHomeResponseDto>> GetCommunityCentersAsync(int quantity)
    {
        var communityCenters = await communityHomeRepository.GetCommunityCentersAsync(quantity);

        if (communityCenters.IsNullOrEmpty())
        {
            throw new NotFoundException("No community centers found");
        }

        return mapper.Map<IEnumerable<CommunityHomeResponseDto>>(communityCenters);
    }

    public async Task<CommunityHomeResponseDto> UpdateCommunityCenterAsync(
        CommunityHomeRequestDto communityCenterRequestDto, Guid id)
    {
        var communityCenter = await communityHomeRepository.GetCommunityCenterAsync(id);

        if (communityCenter == null)
        {
            throw new NotFoundException($"Community Center with ID: {id} not found");
        }

        mapper.Map(communityCenterRequestDto, communityCenter);
        await communityHomeRepository.UpdateCommunityCenterAsync(communityCenter);

        return mapper.Map<CommunityHomeResponseDto>(communityCenter);
    }

    public async Task DeleteCommunityCenterAsync(Guid communityCenterId)
    {
        await communityHomeRepository.DeleteCommunityCenterAsync(communityCenterId);
        await communityHomeRepository.SaveChangesAsync();
    }

    public async Task<List<CommunityHomeWithContracts>> SearchCommunityHomesWithContractsAsync(DateTime from,
        DateTime to)
    {
        var availableHomes = new List<CommunityHomeWithContracts>();

        var communityHomesWithContracts = await contractRepository.GetContractsForSearch(from, to);

        foreach (var home in communityHomesWithContracts)
        {
            // Check if the home is available for every day in the search range
            var isHomeAvailable = true;

            for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
            {
                if (IsDayAvailable(home, date)) continue;
                isHomeAvailable = false;
                break;
            }

            if (isHomeAvailable)
            {
                availableHomes.Add(home);
            }
        }

        return availableHomes;
    }

    private static bool IsDayAvailable(CommunityHomeWithContracts home, DateTime date)
    {
        // Get contracts that overlap with this day
        var overlappingContracts = home.Contracts
            .SelectMany(contract => contract.ContractRanges)
            .Where(range => range.From.Date <= date && range.To.Date >= date)
            .ToList();

        if (overlappingContracts.Count == 0)
        {
            // No overlapping contracts; entire day is available
            return true;
        }

        // Assume the day starts at 00:00 and ends at 23:59
        var totalHoursAvailable = 24;

        // Calculate used hours
        foreach (var contractRange in overlappingContracts)
        {
            // If the contract overlaps with the start of the day, calculate the duration until the end of the day or end of the range
            if (contractRange.From.Date == date)
            {
                var startTime = contractRange.From.TimeOfDay;
                var endTime = (contractRange.To.Date == date) ? contractRange.To.TimeOfDay : TimeSpan.FromHours(24);
                totalHoursAvailable -= (int)(endTime - startTime).TotalHours;
            }
            // If the contract overlaps with the entire day
            else if (contractRange.To.Date == date)
            {
                totalHoursAvailable -= (int)contractRange.To.TimeOfDay.TotalHours;
            }
            // If the contract spans multiple days and covers the entire day
            else
            {
                totalHoursAvailable -= 24;
            }

            // Check if there are still at least 8 hours available
            if (totalHoursAvailable < 8)
            {
                return false;
            }
        }

        return true;
    }
}