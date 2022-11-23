using ArchitecturalStudioTradition.Domain.Photos.Events;
using ArchitecturalStudioTradition.Domain.Photos.Rules;
using ArchitecturalStudioTradition.Domain.SeedWork;

namespace ArchitecturalStudioTradition.Domain.Photos
{
    public class Photo : Entity
    {
        public readonly int? Order;
        public readonly string Filename;
        public readonly string Hash;

        private Photo(string name, string hash, int? order)
        {
            Filename = name;
            Order = order;
            Hash = hash;

            AddDomainEvent(new PhotoCreated(Filename, Hash));
        }

        public static Photo Create(string filename, string hash, int? order = null)
        {
            Validate(new HashMustBeDefined(hash));
            Validate(new OrderMustBeGreaterOrEqualZero(order));

            return new Photo(filename, hash, order);
        }
    }
}
