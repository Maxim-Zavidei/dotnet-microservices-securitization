using System.Text.Json.Serialization;

namespace Jobs.Mvc.Models;

public class Job
{
    [JsonPropertyName("jobId")]
    public int JobId { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
    [JsonPropertyName("company")]
    public string Company { get; set; } = null!;
    [JsonPropertyName("postedDate")]
    public DateTime PostedDate { get; set; }
    [JsonPropertyName("location")]
    public string Location { get; set; } = null!;
}
