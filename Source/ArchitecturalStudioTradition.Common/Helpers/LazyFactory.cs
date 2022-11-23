using Microsoft.Extensions.DependencyInjection;

namespace ArchitecturalStudioTradition.Common.Helpers
{
    public interface ILazy<T>
    {
        T Value { get; }
    }

    public class LazyFactory<T> : ILazy<T> where T : notnull
    {
        private readonly Lazy<T> _lazy;

        public LazyFactory(IServiceProvider service)
        {
            _lazy = new Lazy<T>(() => service.GetRequiredService<T>());
        }

        public T Value => _lazy.Value;
    }
}
