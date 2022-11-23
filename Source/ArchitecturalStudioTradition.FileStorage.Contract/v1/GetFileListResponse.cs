namespace ArchitecturalStudioTradition.FileStorage.Contract.v1
{
    public class GetFileListResponse
    {
        public IReadOnlyCollection<FileModel> Files { get; set; }
    }
}
