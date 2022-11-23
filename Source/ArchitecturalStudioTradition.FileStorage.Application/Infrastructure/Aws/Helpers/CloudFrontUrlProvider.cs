using ArchitecturalStudioTradition.FileStorage.Application.Configuration;

namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Helpers
{
    internal interface ICloudFrontUrlProvider
    {
        Uri GetCloudFrontUrl(string s3ObjectKey);
    }

    internal class CloudFrontUrlProvider : ICloudFrontUrlProvider
    {
        private readonly IAwsConfiguration _configuration;

        public CloudFrontUrlProvider(IAwsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri GetCloudFrontUrl(string s3ObjectKey)
        {
            return new Uri(_configuration.CloudFrontUrl, s3ObjectKey);
        }
    }
}
