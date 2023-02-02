using Jobs.API.Data;
using Jobs.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<StartupConfig>(c => builder.Configuration.GetSection("StartupConfig"));

builder.Services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", opt =>
{
    opt.RequireHttpsMetadata = false;
    // We indicate the token must come from the authorization server at this URL
    opt.Authority = "https://localhost:7000";
    opt.Audience = "jobsapi";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var startupConfig = app.Services.GetService<StartupConfig>();
if (startupConfig != null && startupConfig.RunDbMigrations)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        context.Database.Migrate();
    }
}

if (true)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        DatabaseInitializer.Initialize(context);
    }
}

app.Run();
