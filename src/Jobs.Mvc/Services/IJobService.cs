using Jobs.Mvc.Models;

namespace Jobs.Mvc.Services;

public interface IJobService
{
    Task<Job> GetJob(int jobId);
    Task<IEnumerable<Job>> GetJobs();
}
