using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Exceptions;
using BddAPI.Models;
using BddAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BddAPI.Services;

public interface IRecordService
{
    Task<RecordResponseDto> CreateRecordAsync(RecordRequestDto recordRequestDto);
    Task<RecordResponseDto> GetRecordByIdAsync(Guid recordId);
    Task<List<RecordResponseDto>?> GetRecordsAsync();
    Task<RecordResponseDto> UpdateRecordAsync(Guid recordId, RecordRequestDto recordRequestDto);
    Task<List<RecordResponseDto>> GetUserRecordsAsync(Guid userId);
    Task DeleteRecordAsync(Guid recordId);
}

public class RecordService(IRecordRepository recordRepository, IMapper mapper) : IRecordService
{
    public async Task<RecordResponseDto> CreateRecordAsync(RecordRequestDto recordRequestDto)
    {
        var record = mapper.Map<Record>(recordRequestDto);

        await recordRepository.CreateRecordAsync(record);
        await recordRepository.SaveChangesAsync();

        return mapper.Map<RecordResponseDto>(record);
    }

    public async Task<RecordResponseDto> GetRecordByIdAsync(Guid recordId)
    {
        var record = await recordRepository.GetRecordByIdAsync(recordId);

        if (record == null)
        {
            throw new NotFoundException($"Record with ID: {recordId} not found");
        }

        return mapper.Map<RecordResponseDto>(record);
    }

    public async Task<List<RecordResponseDto>?> GetRecordsAsync()
    {
        var records = await recordRepository.GetRecordsAsync();

        if (records.IsNullOrEmpty())
        {
            throw new NotFoundException("No records found");
        }

        return mapper.Map<List<RecordResponseDto>>(records);
    }

    public async Task<RecordResponseDto> UpdateRecordAsync(Guid recordId, RecordRequestDto recordRequestDto)
    {
        var record = await recordRepository.GetRecordByIdAsync(recordId);

        if (record == null)
        {
            throw new NotFoundException($"Record with ID: {recordId} not found");
        }

        record = mapper.Map(recordRequestDto, record);

        await recordRepository.UpdateRecordAsync(record);
        await recordRepository.SaveChangesAsync();

        return mapper.Map<RecordResponseDto>(record);
    }

    public async Task<List<RecordResponseDto>> GetUserRecordsAsync(Guid userId)
    {
        var records = await recordRepository.GetUserRecordsAsync(userId);

        if (records.IsNullOrEmpty())
        {
            throw new NotFoundException("No records found");
        }

        return mapper.Map<List<RecordResponseDto>>(records);
    }

    public async Task DeleteRecordAsync(Guid recordId)
    {
        var record = await recordRepository.GetRecordByIdAsync(recordId);

        if (record == null)
        {
            throw new NotFoundException($"Record with ID: {recordId} not found");
        }

        await recordRepository.DeleteRecordAsync(record);
        await recordRepository.SaveChangesAsync();
    }
}