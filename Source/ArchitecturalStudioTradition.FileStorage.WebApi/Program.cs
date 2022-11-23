using AArchitecturalStudioTradition.FileStorage.WebApi;
using Amazon.Extensions.NETCore.Setup;
using ArchitecturalStudioTradition.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

AWSOptions awsOptions = builder.Configuration.GetAWSOptions();

builder.Services.AddApi();
builder.Services.AddAws(awsOptions);
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
