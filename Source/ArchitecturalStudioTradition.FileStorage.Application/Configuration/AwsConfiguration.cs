namespace ArchitecturalStudioTradition.FileStorage.Application.Configuration
{
    public interface IAwsConfiguration
    {
        string AwsBucketName { get; }
        string AwsRegionSystemName { get; }
        Uri CloudFrontUrl { get; }
    }
}
