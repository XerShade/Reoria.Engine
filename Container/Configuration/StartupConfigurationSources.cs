using Reoria.Engine.Container.Configuration.Interfaces;

namespace Reoria.Engine.Container.Configuration;

public sealed class StartupConfigurationSources(IEngineConfigurationProvider provider) : EngineConfigurationSources(provider)
{
    protected override void AddSources() 
        => base.AddSources();
}
