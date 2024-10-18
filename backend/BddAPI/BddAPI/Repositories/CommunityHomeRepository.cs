using BddAPI.Data;
using BddAPI.Exceptions;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Repositories;

public interface ICommunityHomeRepository
{
    Task<CommunityHome> CreateCommunityCenterAsync(CommunityHome communityCenter);
    Task<CommunityHome?> GetCommunityCenterAsync(Guid id);
    Task<IEnumerable<CommunityHome?>> GetCommunityCentersAsync(int quantity);
    Task<IEnumerable<CommunityHome>> GetAllCommunityCentersAsync();
    Task<CommunityHome> UpdateCommunityCenterAsync(CommunityHome communityCenter);
    Task DeleteCommunityCenterAsync(Guid id);
    Task SaveChangesAsync();
}

public class CommunityHomeRepository(BddDbContext dbContext) : ICommunityHomeRepository
{
    public async Task<CommunityHome> CreateCommunityCenterAsync(CommunityHome communityCenter)
    {
        await dbContext.CommunityCenters.AddAsync(communityCenter);
        return communityCenter;
    }

    public async Task<CommunityHome?> GetCommunityCenterAsync(Guid id)
    {
        return await dbContext.CommunityCenters.FindAsync(id);
    }

    public async Task<IEnumerable<CommunityHome?>> GetCommunityCentersAsync(int quantity)
    {
        if (quantity == 0)
        {
            return await dbContext.CommunityCenters.ToListAsync();
        }

        return await dbContext.CommunityCenters.Take(quantity).ToListAsync();
    }

    public async Task<IEnumerable<CommunityHome>> GetAllCommunityCentersAsync()
    {
        return await dbContext.CommunityCenters.ToListAsync();
    }

    public async Task<CommunityHome> UpdateCommunityCenterAsync(CommunityHome communityCenter)
    {
        dbContext.CommunityCenters.Update(communityCenter);
        await dbContext.SaveChangesAsync();
        return communityCenter;
    }

    public async Task DeleteCommunityCenterAsync(Guid id)
    {
        var communityCenter = await dbContext.CommunityCenters.FindAsync(id);

        if (communityCenter == null)
        {
            throw new NotFoundException($"Community Center with ID: {id} not found");
        }

        dbContext.CommunityCenters.Remove(communityCenter);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}