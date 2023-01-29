using System.Text.Json;
using Jobs.Mvc.Config;
using Jobs.Mvc.Http;
using Jobs.Mvc.Models;
using Microsoft.Extensions.Options;

namespace Jobs.Mvc.Services;

public class JobService : IJobService
{
    private readonly IHttpClient apiClient;
    private readonly ApiConfig apiConfig;

    public JobService(IHttpClient apiClient, IOptionsMonitor<ApiConfig> apiConfig)
    {
        this.apiClient = apiClient;
        this.apiConfig = apiConfig.CurrentValue;
    }
    
    public async Task<Job> GetJob(int jobId)
    {
        var dataString = await apiClient.GetStringAsync(apiConfig.JobsApiUrl + "/jobs/" + jobId);
        return JsonSerializer.Deserialize<Job>(dataString)!;
    }

    public async Task<IEnumerable<Job>> GetJobs()
    {
        var dataString = await apiClient.GetStringAsync(apiConfig.JobsApiUrl + "/jobs/");
        return JsonSerializer.Deserialize<IEnumerable<Job>>(dataString)!;
    }
}
