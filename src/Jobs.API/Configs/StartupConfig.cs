namespace Jobs.API.Configs;

public class StartupConfig
{
    public bool RunDbMigrations { get; set; }
    public bool SeedDatabase { get; set; }
}
