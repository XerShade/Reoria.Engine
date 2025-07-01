using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Container.Interfaces;

public interface IEngineContainer : IDisposable, IAsyncDisposable
{
    IConfiguration Configuration { get; }
    ILogger<IEngineContainer> Logger { get; }
    IServiceProvider Provider { get; }
}
