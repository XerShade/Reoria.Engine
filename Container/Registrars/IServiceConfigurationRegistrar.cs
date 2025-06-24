namespace Reoria.Engine.Container.Registrars;

/// <summary>
/// Defines a contract for configuring services after dependency injection has been set up.
/// </summary>
public interface IServiceConfigurationRegistrar
{
    /// <summary>
    /// Configures additional services using the given service provider.
    /// This is typically used to perform setup that requires already-constructed services.
    /// </summary>
    /// <param name="provider">An <see cref="IServiceProvider"/> used to resolve services.</param>
    void ConfigureServices(IServiceProvider provider);
}