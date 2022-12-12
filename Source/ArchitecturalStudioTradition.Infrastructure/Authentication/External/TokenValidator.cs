using ArchitecturalStudioTradition.Model.UserIdentity;

namespace ArchitecturalStudioTradition.Infrastructure.Authentication.External
{
    public interface ITokenValidator<T>
    {
        bool ForProvider(ExternalAuthProvider provider);
        Task<T> ValidateTokenAsync(string token);
    }

    public abstract class BaseTokenValidator
    {
        //public BaseTokenValidator(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}
    }
}
