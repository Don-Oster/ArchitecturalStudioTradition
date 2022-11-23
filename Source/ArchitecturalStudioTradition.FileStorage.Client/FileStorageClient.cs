using System.Text;
using ArchitecturalStudioTradition.FileStorage.Contract.v1;
using Newtonsoft.Json;

namespace ArchitecturalStudioTradition.FileStorage.Client
{
    public interface IFileStorageClient
    {
        Task<GetFileListResponse> GetFileList(GetFileListRequest request);
        Task<GetFileResponse> GetFile(GetFileRequest request);
        Task<string> SaveFile(SaveFileRequest request);
    }

    public class FileStorageClient : IFileStorageClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _serviceUrl;

        public FileStorageClient(HttpClient httpClient, Uri serviceUrl) { 
        
            _httpClient = httpClient;
            _serviceUrl = serviceUrl;
        }

        public Task<GetFileListResponse> GetFileList(GetFileListRequest request)
        {
            return Get<GetFileListResponse>(FileStorageUrlProvider.GetFileList(request));
        }

        public Task<GetFileResponse> GetFile(GetFileRequest request)
        {
            return Get<GetFileResponse>(FileStorageUrlProvider.GetFile(request));
        }

        public Task<string> SaveFile(SaveFileRequest request)
        {
            return Post<SaveFileRequest, string>(FileStorageUrlProvider.SaveFile(), request);
        }

        private async Task<TResult> Get<TResult>(string relativeUrl)
        {
            using var response = await _httpClient.GetAsync(GetFullUrl(relativeUrl));
            return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
        }

        private async Task<TResult> Post<TRequest, TResult>(string relativeUrl, TRequest request)
        {
            using var response = await _httpClient.PostAsync(GetFullUrl(relativeUrl), new StringContent(
                JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
        }

        private Uri GetFullUrl(string relativeUrl)
        {
            return new Uri(_serviceUrl, relativeUrl);
        }
    }
}