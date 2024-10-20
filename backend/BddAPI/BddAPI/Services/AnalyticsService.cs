using BddAPI.DTOs.Response;

namespace BddAPI.Services;

public interface IAnalyticsService
{
    Task<AnalyticsResponse> GetUsageAnalytics();
}

public class AnalyticsService : IAnalyticsService
{
    public Task<AnalyticsResponse> GetUsageAnalytics()
    {
        throw new NotImplementedException();
    }
}