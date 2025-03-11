using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Services.Interfaces;

public interface IEngineServiceContainer : IDisposable, IAsyncDisposable
{
    ServiceProvider Provider { get; }
    ServiceCollection Services { get; }

    IEngineServiceContainer RegisterServices();
    IEngineServiceContainer BuildServiceProvider();
    IEngineServiceContainer ConfigureServices();
    IEngineServiceContainer DiscoverServices();
}
