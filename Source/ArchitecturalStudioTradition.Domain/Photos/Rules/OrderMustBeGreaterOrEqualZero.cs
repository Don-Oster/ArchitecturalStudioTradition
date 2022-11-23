using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.Photos.Rules
{
    public class OrderMustBeGreaterOrEqualZero: IBusinessRule
    {
        private readonly int? _order;

        public OrderMustBeGreaterOrEqualZero(int? order)
        {
            _order = order;
        }

        public bool IsValid()
        {
            return !_order.HasValue || _order >= 0;
        }

        public string ValidationErrorMessage => "Order must be greater or equal to zero.";
    }
}
