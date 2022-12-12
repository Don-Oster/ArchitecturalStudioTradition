using ArchitecturalStudioTradition.Identity.Application.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityServer();
builder.Services
    .AddDbContext<IdentityContext>(options => options
    .UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
        x => x.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name)));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseIdentityServer();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
