using BddAPI.Data;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Repositories;

public interface IRecordRepository
{
    Task<Record> CreateRecordAsync(Record record);
    Task<Record?> GetRecordByIdAsync(Guid recordId);
    Task<List<Record>?> GetRecordsAsync();
    Task<Record> UpdateRecordAsync(Record record);
    Task DeleteRecordAsync(Record record);
    Task<List<Record>?> GetUserRecordsAsync(Guid userId);
    Task SaveChangesAsync();
}

public class RecordRepository(BddDbContext dbContext) : IRecordRepository

{
    public async Task<Record> CreateRecordAsync(Record record)
    {
        await dbContext.Records.AddAsync(record);
        return record;
    }

    public async Task<Record?> GetRecordByIdAsync(Guid recordId)
    {
        return await dbContext.Records
            .Include(r => r.User)
            .Include(r => r.Contract)
            .FirstOrDefaultAsync(r => r.Id == recordId);
    }

    public async Task<List<Record>?> GetRecordsAsync()
    {
        return await dbContext.Records
            .Include(r => r.User)
            .Include(r => r.Contract)
            .ToListAsync();
    }

    public async Task<Record> UpdateRecordAsync(Record record)
    {
        dbContext.Records.Update(record);
        await dbContext.SaveChangesAsync();
        return record;
    }

    public async Task DeleteRecordAsync(Record record)
    {
        dbContext.Records.Remove(record);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Record>?> GetUserRecordsAsync(Guid userId)
    {
        return await dbContext.Records
            .Include(r => r.User)
            .Include(r => r.Contract)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}