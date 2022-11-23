using ArchitecturalStudioTradition.FileStorage.Application.Configuration;
using System.ComponentModel.DataAnnotations;

namespace ArchitecturalStudioTradition.WebApi.Configuration
{
    public class WebApiConfiguration : IAwsConfiguration
    {
        private const string ConfigurationPrefix = "ARCHTRADITION:FILESTORAGE";

        #region IAwsConfiguration

        [Required]
        public string AwsBucketName { get; set; }

        [Required]
        public string AwsRegionSystemName { get; set; }

        [Required]
        public Uri CloudFrontUrl { get; set; }

        #endregion

        private WebApiConfiguration(IConfiguration configuration)
        {
            configuration.Bind(ConfigurationPrefix, this);
        }

        public static WebApiConfiguration GetInstance(IConfiguration configuration)
        {
            var config = new WebApiConfiguration(configuration);
            Validator.ValidateObject(config, new ValidationContext(config), true);
            return config;
        }

        public static WebApiConfiguration GetInstance()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                                  Environments.Development;

            var configurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .Build();

            return GetInstance(configurationRoot);
        }
    }
}
