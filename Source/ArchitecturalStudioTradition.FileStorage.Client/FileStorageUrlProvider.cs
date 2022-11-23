using ArchitecturalStudioTradition.FileStorage.Contract.v1;

namespace ArchitecturalStudioTradition.FileStorage.Client
{
    internal static class FileStorageUrlProvider
    {
        private const string VersionedApi = "v1";

        internal static string GetFileList(GetFileListRequest request)
        {
            return $"{VersionedApi}/files/list?hash={request.Hash}";
        }

        internal static string GetFile(GetFileRequest request)
        {
            return $"{VersionedApi}/files?hash={request.Hash}";
        }

        internal static string SaveFile()
        {
            return $"{VersionedApi}/files/save";
        }
    }
}