using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Services.Interfaces;

/// <summary>
/// Defines the contract for loading and configuring services in the engine.
/// </summary>
public interface IEngineServiceLoader
{
    /// <summary>
    /// Adds services to the specified <see cref="IServiceCollection"/>.
    /// This method is typically used to register dependencies with the DI container.
    /// </summary>
    /// <param name="services">The collection of services to which services should be added.</param>
    void AddServices(IServiceCollection services);

    /// <summary>
    /// Configures services after they have been added to the <see cref="IServiceProvider"/>.
    /// This method is used to perform any additional setup or configuration that requires access
    /// to the fully constructed service provider.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/> used to resolve dependencies.</param>
    void ConfigureServices(IServiceProvider provider);
}
