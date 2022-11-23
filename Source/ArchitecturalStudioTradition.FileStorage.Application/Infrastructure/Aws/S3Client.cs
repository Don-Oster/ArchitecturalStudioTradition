using Amazon;
using Amazon.S3;
using ArchitecturalStudioTradition.FileStorage.Application.Configuration;
using Microsoft.Extensions.Logging;

namespace PgcrDashboards.Admin.Web.Infrastructure
{
    internal interface IS3Client
    {
        Task<TResult> PerformOperationAsync<TResult>(Func<IAmazonS3, Task<TResult>> action, string operationDescription);
    }

    internal class S3Client : IS3Client
    {
        private readonly AmazonS3Client _client;
        private readonly ILogger<S3Client> _logger;

        public S3Client(IAwsConfiguration configuration, ILogger<S3Client> logger)
        {
            _client = GetClient(configuration);
            _logger = logger;
        }

        public async Task<TResult> PerformOperationAsync<TResult>(Func<IAmazonS3, Task<TResult>> action, string operationDescription)
        {
            try
            {
                return await action(_client);
            }
            catch (AmazonS3Exception exception)
            {
                _logger.LogError($"Error occurred while {operationDescription}.", exception);
                throw;
            }
        }

        #region Private Methods

        private static AmazonS3Client GetClient(IAwsConfiguration config)
        {
            var regionEndpoint = RegionEndpoint.GetBySystemName(config.AwsRegionSystemName);
            return new AmazonS3Client(regionEndpoint);
        }

        #endregion
    }
}