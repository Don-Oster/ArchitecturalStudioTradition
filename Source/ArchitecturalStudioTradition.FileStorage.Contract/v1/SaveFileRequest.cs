namespace ArchitecturalStudioTradition.FileStorage.Contract.v1
{
    public class SaveFileRequest
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}