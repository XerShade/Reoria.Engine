using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Base.Container.Logging.Interfaces;

public interface IContainerLoggingInitializer : IDisposable, IAsyncDisposable
{
    ILoggerFactory Initialize();
}
