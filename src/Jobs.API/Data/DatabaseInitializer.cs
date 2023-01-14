using Jobs.API.Models;

namespace Jobs.API.Data;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationContext context)
    {
        context.Database.EnsureCreated();
        if (context.Jobs.Any())
        {
            // Database already has data.
            return;
        }
        var jobs = new Job []
        {
            new Job
            {
                Title = "Ttitle1",
                Company = "Company1",
                Description = "Description1",
                PostedDate = new DateTime().ToUniversalTime()
            },
            new Job
            {
                Title = "Ttitle2",
                Company = "Company2",
                Description = "Description2",
                PostedDate = DateTime.UtcNow
            },
            new Job
            {
                Title = "Ttitle3",
                Company = "Company3",
                Description = "Description3",
                PostedDate = DateTime.UtcNow
            },
            new Job
            {
                Title = "Ttitle4",
                Company = "Company4",
                Description = "Description4",
                PostedDate = DateTime.UtcNow
            }
        };
        context.Jobs.AddRange(jobs);
        context.SaveChanges();
    }
}
