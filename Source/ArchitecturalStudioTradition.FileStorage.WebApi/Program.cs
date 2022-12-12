using AArchitecturalStudioTradition.FileStorage.WebApi;
using Amazon.Extensions.NETCore.Setup;
using ArchitecturalStudioTradition.FileStorage.WebApi.Services;
using ArchitecturalStudioTradition.WebApi.Extensions;
using Google.Api;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AWSOptions awsOptions = builder.Configuration.GetAWSOptions();

builder.Services.AddApi();
builder.Services.AddAws(awsOptions);
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC file storage", Version = "v1" });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Server.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "File Storage API V1");
});
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors();
app.MapGrpcReflectionService();
app.MapGrpcService<FileStorageClientImplementation>().RequireCors("AllowAll"); ;
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
