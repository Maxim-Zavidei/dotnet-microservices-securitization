using System.ComponentModel.DataAnnotations;

namespace Jobs.API.Models;

public class Job
{
    [Key]
    public int JobId { get; set; }
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime PostedDate { get; set; }
    public string Location { get; set; } = null!;
}
