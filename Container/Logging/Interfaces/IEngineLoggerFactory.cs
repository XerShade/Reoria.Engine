using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Container.Logging.Interfaces;

public interface IEngineLoggerFactory : IDisposable, IAsyncDisposable
{
    ILogger<T> GetLogger<T>();
    ILoggerFactory GetLoggerFactory();
    IEngineLoggerFactory SetupFactory(IConfiguration configuration);
}
