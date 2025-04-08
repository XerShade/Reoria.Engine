using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Container.Configuration.Interfaces;

public interface IEngineConfigurationProvider : IDisposable, IAsyncDisposable
{
    bool EnvironmentVariables { get; }
    bool UserSecrets { get; }
    string Environment { get; }
    string Version { get; }

    IConfigurationBuilder CreateConfigurationBuilder();
    IConfigurationBuilder CreateEarlyConfigurationBuilder();
}
