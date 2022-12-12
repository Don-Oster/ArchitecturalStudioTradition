using ArchitecturalStudioTradition.CQRS;
using ArchitecturalStudioTradition.Database;
using ArchitecturalStudioTradition.Infrastructure.ErrorHandling;
using ArchitecturalStudioTradition.Model.UserIdentity;
using ArchitecturalStudioTradition.WebApi;
using ArchitecturalStudioTradition.WebApi.Configuration;
using ArchitecturalStudioTradition.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using System.Text;
using ArchitecturalStudioTradition.Common.Telemetry;

var builder = WebApplication.CreateBuilder(args);

var config = WebApiConfiguration.GetInstance();

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCQRS(typeof(Program).Assembly);
builder.Services.AddApi(config);
builder.Services.ConfigureProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddOpenTelemetry();
builder.Services.AddApplicationGraphQL();
//    .AddCheck<CustomHealthCheck>("API")
//    .AddNpgSql();

var identityBuilder = builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = false;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    opt.Lockout.MaxFailedAccessAttempts = 3;
}).AddDefaultTokenProviders();

builder.Services.AddPostgreSQLDatabase(config, identityBuilder);

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddCors(opt => opt.AddPolicy(name: "CorsPolicy",
cfg => {
    cfg.AllowAnyHeader();
    cfg.AllowAnyMethod();
    cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
}));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddFacebook(options =>
    {
        options.AppId = config.FacebookAppId;
        options.AppSecret = config.FacebookAppSecret;
        options.SignInScheme = "Bearer";
    })
    .AddGoogle(options =>
    {
        options.ClientId = config.GoogleClientId;
        options.ClientSecret = config.GoogleClientSecret;
        options.SignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(jwtSettings.GetSection("securityKey").Value))
        };
    });

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Lockout.AllowedForNewUsers = true;
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    opts.Lockout.MaxFailedAccessAttempts = 3;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.MapMetrics();

app.MapControllers();
app.MapMethods("/api/heartbeat", new[] { "HEAD" }, () => Results.Ok());

app.Run();
