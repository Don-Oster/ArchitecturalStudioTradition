using Microsoft.Extensions.Configuration;

namespace ArchitecturalStudioTradition.FileStorage.Client
{
    public class FileStorageClientConfiguration
    {
        private const string ConfigurationPrefix = "ARCHITECTURALSTUDIO:TRADITION:CLIENT";

        private Uri _baseAddress;

        public FileStorageClientConfiguration(IConfiguration configuration)
        {
            configuration.Bind(ConfigurationPrefix, this);
        }

        public Uri BaseAddress
        {
            get => _baseAddress;
            set => _baseAddress = EnsureTrailingSlash(value);
        }

        private static Uri? EnsureTrailingSlash(Uri? url)
        {
            if (url == null) return null;

            var urlString = url.ToString();
            if (urlString.EndsWith("/")) return url;

            return new Uri($"{urlString}/");
        }
    }
}