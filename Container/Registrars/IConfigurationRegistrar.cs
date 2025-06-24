using Reoria.Engine.Container.Configuration.Interfaces;

namespace Reoria.Engine.Container.Registrars;

/// <summary>
/// Defines a contract for registering configuration sources to the application's dependency injection container.
/// </summary>
public interface IConfigurationRegistrar
{
    /// <summary>
    /// Registers configuration sources used by the application.
    /// This allows extending the application's configuration loading behavior.
    /// </summary>
    /// <param name="sources">The application configuration sources collection to register.</param>
    void RegisterSources(IEngineConfigurationSources sources);
}