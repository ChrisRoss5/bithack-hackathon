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
        // Initialize an array to represent each hour of the day (0 to 23)
        var hours = new bool[24]; // false indicates available, true indicates booked

        // Mark booked hours based on overlapping contract ranges
        foreach (var contract in home.Contracts)
        {
            foreach (var range in contract.ContractRanges)
            {
                // Skip ranges that don't overlap with the current date
                if (range.From.Date > date || range.To.Date < date)
                    continue;

                // Determine the start and end hours for the booking on this date
                var startHour = (range.From.Date == date) ? range.From.Hour : 0;
                var endHour = (range.To.Date == date) ? range.To.Hour : 24;

                // Mark the hours as booked
                for (int i = startHour; i < endHour; i++)
                {
                    hours[i] = true; // true indicates booked
                }
            }
        }

        // Check for a continuous block of at least 8 available hours
        var continuousAvailableHours = 0;
        for (var i = 0; i < 24; i++)
        {
            if (!hours[i]) // Hour is available
            {
                continuousAvailableHours++;
                if (continuousAvailableHours >= 8)
                    return true; // Found a continuous block of at least 8 hours
            }
            else
            {
                continuousAvailableHours = 0; // Reset counter if hour is booked
            }
        }

        // No continuous block of at least 8 hours found
        return false;
    }
}