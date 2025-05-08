using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Container.Configuration.Interfaces;

public interface IEngineConfigurationProvider : IDisposable, IAsyncDisposable
{
    bool EnvironmentVariables { get; }
    bool UserSecrets { get; }
    string Environment { get; }
    string Version { get; }

    IConfigurationBuilder AddEnvironmentJsonFile(string path, bool reloadOnChange);
    IConfigurationBuilder AddJsonFile(string path, bool optional, bool reloadOnChange);
    IConfigurationBuilder GetConfigurationBuilder();
}
