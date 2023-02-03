using Jobs.Api.Models;

namespace Jobs.Api.Data;

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
                PostedDate = new DateTime().ToUniversalTime(),
                Location = "Street 123"
            },
            new Job
            {
                Title = "Ttitle2",
                Company = "Company2",
                Description = "Description2",
                PostedDate = DateTime.UtcNow,
                Location = "Street 321"
            },
            new Job
            {
                Title = "Ttitle3",
                Company = "Company3",
                Description = "Description3",
                PostedDate = DateTime.UtcNow,
                Location = "Street 456"
            },
            new Job
            {
                Title = "Ttitle4",
                Company = "Company4",
                Description = "Description4",
                PostedDate = DateTime.UtcNow,
                Location = "Street 543"
            }
        };
        context.Jobs.AddRange(jobs);
        context.SaveChanges();
    }
}
