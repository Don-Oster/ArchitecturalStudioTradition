using ArchitecturalStudioTradition.Database.Configuration;
using ArchitecturalStudioTradition.Infrastructure.Configuration;
using System.ComponentModel.DataAnnotations;

namespace ArchitecturalStudioTradition.WebApi.Configuration
{
    public record WebApiConfiguration : IPostgreSqlConfig, IJwtTokenConfiguration, IGoogleAuthConfiguration, IFacebookAuthConfiguration, IEmailConfiguration
    {
        private const string ConfigurationPrefix = "ARCHTRADITION";

        #region IPostgreSqlConfig

        [Required]
        public string DbHost { get; set; }
        [Required]
        public int DbPort { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        public string DbUsername { get; set; }
        [Required]
        public string DbPassword { get; set; }

        #endregion

        #region IJwtTokenConfiguration

        [Required]
        public string JwtSecurityKey { get; set; }
        [Required]
        public string JwtIssuer { get; set; }
        [Required]
        public string JwtAudience { get; set; }
        [Required]
        public int JwtExpiryInMinutes { get; set; }

        #endregion

        #region IGoogleAuthConfiguration

        [Required]
        public string GoogleClientId { get; set; }
        [Required]
        public string GoogleClientSecret { get; set; }

        #endregion

        #region IFacebookAuthConfiguration

        [Required]
        public string FacebookAppId { get; set; }
        [Required]
        public string FacebookAppSecret { get; set; }

        #endregion

        #region IEmailConfiguration

        [Required]
        public string EmailFromName { get; set; }
        [Required]
        public string EmailFromAddress { get; set; }
        [Required]
        public string EmailSmtpServer { get; set; }
        [Required]
        public int EmailPort { get; set; }
        [Required]
        public string EmailUserName { get; set; }
        [Required]
        public string EmailPassword { get; set; }

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
