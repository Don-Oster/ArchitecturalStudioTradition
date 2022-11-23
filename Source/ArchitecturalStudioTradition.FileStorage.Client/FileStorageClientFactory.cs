namespace ArchitecturalStudioTradition.FileStorage.Client
{
    public static class FileStorageClientFactory
    {
        private static readonly Lazy<HttpClient> Client = new(() =>
                new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(10)
                });

        public static IFileStorageClient Create(Uri serviceUrl)
        {
            var url = serviceUrl ?? throw new ArgumentException("serviceUrl must be provided");
            return new FileStorageClient(Client.Value, url);
        }
    }
}