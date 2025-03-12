namespace Reoria.Engine.Base.Container.Interfaces;

public interface IEngineContainer : IDisposable, IAsyncDisposable
{
    IEngineContainer DiscoverContainerServiceClasses();
    IEngineContainer DiscoverConfigurationSources();
    IEngineContainer BuildContainerConfiguration();
    IEngineContainer BuildContainerLogger();
}
