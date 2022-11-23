using ArchitecturalStudioTradition.Domain.SeedWork.Events;

namespace ArchitecturalStudioTradition.Domain.Photos.Events
{
    public class PhotoCreated : DomainEvent
    {
        public PhotoCreated(string filename, string hash)
        {
            Filename = filename;
            Hash = hash;
        }

        public string Filename { get; }
        public string Hash { get; }
    }
}