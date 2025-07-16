using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Registrars;

namespace Reoria.Engine.Container.Configuration.Registrars;

public class EngineConfigurationRegistrar : IConfigurationRegistrar
{
    public void RegisterSources(IEngineConfigurationSources sources) 
        => sources.AddSource("appsettings.json", optional: false, reloadOnChange: true);
}
