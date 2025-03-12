namespace Reoria.Engine.Base.Container.Interfaces;

public interface IEngineContainer : IDisposable, IAsyncDisposable
{
    IEngineContainer DiscoverContainerServiceClasses();
    IEngineContainer DiscoverConfigurationSources();
    IEngineContainer BuildContainerConfiguration();
    IEngineContainer BuildContainerLogger();
    IEngineContainer DiscoverContainerServices();
    IEngineContainer BuildContainerServices();
    IEngineContainer BuildContainerServiceProvider();
    TService RetrieveService<TService>() where TService : class;
}
