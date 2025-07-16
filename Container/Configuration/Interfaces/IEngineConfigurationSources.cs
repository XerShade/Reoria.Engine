using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Container.Configuration.Interfaces;

public interface IEngineConfigurationSources
{
    void AddSource(string path, bool optional, bool reloadOnChange);
    IConfiguration GetConfiguration();
}
