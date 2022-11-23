using ArchitecturalStudioTradition.Model.UserIdentity;
using AutoMapper;

namespace ArchitecturalStudioTradition.Infrastructure.Authentication.External
{
    public interface ITokenValidator<T>
    {
        bool ForProvider(ExternalAuthProvider provider);
        Task<T> ValidateTokenAsync(string token);
    }

    public abstract class BaseTokenValidator
    {
        private readonly IMapper _mapper;
        public BaseTokenValidator(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
