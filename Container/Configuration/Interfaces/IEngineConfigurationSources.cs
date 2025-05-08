using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Container.Configuration.Interfaces;

public interface IEngineConfigurationSources
{
    IConfiguration GetConfiguration();
}
