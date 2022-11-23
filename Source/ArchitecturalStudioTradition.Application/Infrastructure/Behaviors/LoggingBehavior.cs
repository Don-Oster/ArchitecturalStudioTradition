using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ArchitecturalStudioTradition.Application.Infrastructure.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly JsonSerializerSettings _settings;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(Format("Handling started"));
            _logger.LogInformation($"{ObjectInfo(request)}");
            try
            {
                var response = await next();

                _logger.LogInformation(Format("Handled"));
                _logger.LogInformation(Format($"{ObjectInfo(response)}"));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(Format("Handling error"), ex);
                throw;
            }
        }

        private static string Format(string message) => $"[{nameof(Mediator)}:{typeof(TRequest).Name}] {message}";

        private string ObjectInfo(object o) => o == null
            ? "<null>"
            : JsonConvert.SerializeObject(o, Formatting.Indented, _settings);
    }
}
