using System.Text.Json;
using Jobs.Mvc.Config;
using Jobs.Mvc.Infrastructure;
using Jobs.Mvc.Models;
using Microsoft.Extensions.Options;

namespace Jobs.Mvc.Services;

public class JobService : IJobService
{
    private readonly HttpClient apiClient;
    private readonly ApiConfig apiConfig;
    private readonly string remoteServiceBaseUrl;

    public JobService(HttpClient apiClient, IOptionsMonitor<ApiConfig> apiConfig)
    {
        this.apiClient = apiClient;
        this.apiConfig = apiConfig.CurrentValue;
        this.remoteServiceBaseUrl = $"{this.apiConfig.JobsApiUrl}/Jobs";
    }
    
    public async Task<Job> GetJob(int jobId)
    {
        var uri = API.Job.GetJob(remoteServiceBaseUrl, jobId);
        var responseString = await apiClient.GetStringAsync(uri);

        var job = JsonSerializer.Deserialize<Job>(responseString)!;
        return job;
    }

    public async Task<IEnumerable<Job>> GetJobs()
    {
        var uri = API.Job.GetAllJobs(remoteServiceBaseUrl);
        var responseString = await apiClient.GetStringAsync(uri);
        return JsonSerializer.Deserialize<IEnumerable<Job>>(responseString)!;
    }
}
