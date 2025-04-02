using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Container.Logging.Interfaces;

public interface ILoggingInitializer : IDisposable, IAsyncDisposable
{
    ILoggerFactory CreateLoggerFactory();
}
