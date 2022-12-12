using ArchitecturalStudioTradition.Common.Extensions;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddJwtAuthentication();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("proxy"));

app.UseCorrelationId();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapReverseProxy(builder =>
{
    builder.Use(async (context, next) =>
    {
        var token = await context.GetTokenAsync("access_token");
        context.Request.Headers["Authorization"] = $"Bearer {token}";

        await next().ConfigureAwait(false);
    });
});

app.Run();