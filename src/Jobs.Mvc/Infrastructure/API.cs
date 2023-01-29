namespace Jobs.Mvc.Infrastructure;

public static class API
{
    public static class Job
    {
        public static string GetJob(string baseUri, int jobId)
        {
            return $"{baseUri}/{jobId}";
        }

        public static string GetAllJobs(string baseUri)
        {
            return baseUri;
        }
    }
}
