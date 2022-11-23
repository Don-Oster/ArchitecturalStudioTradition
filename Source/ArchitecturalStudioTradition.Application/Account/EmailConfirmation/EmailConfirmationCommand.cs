using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class EmailConfirmationCommand : ICommand<bool>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
