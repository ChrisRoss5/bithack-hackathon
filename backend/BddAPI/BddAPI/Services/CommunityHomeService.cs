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

    Task<IEnumerable<CommunityHomeResponseDto>> GetCommunityCentersByAvailabilityAsync(DateTime startDate,
        DateTime endDate);

    Task<CommunityHomeResponseDto> UpdateCommunityCenterAsync(CommunityHomeRequestDto communityCenterRequestDto,
        Guid id);

    Task DeleteCommunityCenterAsync(Guid communityCenterId);
}

public class CommunityHomeService(ICommunityHomeRepository communityHomeRepository, IMapper mapper)
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

    public Task<IEnumerable<CommunityHomeResponseDto>> GetCommunityCentersByAvailabilityAsync(DateTime startDate,
        DateTime endDate)
    {
        throw new NotImplementedException();
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
}