using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Services.Interfaces;

/// <summary>
/// Defines the contract for a service container that is responsible for managing and registering services in the engine's dependency injection container.
/// It handles the initialization, loading, adding, and configuring of services through loaders and a service provider.
/// </summary>
public interface IEngineServiceContainer : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The service provider created from the service collection.
    /// </summary>
    ServiceProvider Provider { get; }
    /// <summary>
    /// The collection of services registered within the container.
    /// </summary>
    ServiceCollection Services { get; }

    /// <summary>
    /// Adds services to the container by calling the <see cref="IEngineServiceLoader.AddServices"/> method for each discovered loader.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer AddServices();
    /// <summary>
    /// Builds the <see cref="ServiceProvider"/> from the registered services. Once this method is called, no more services can be added to the container.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service collection is empty when attempting to build the provider.</exception>
    IEngineServiceContainer BuildServiceProvider();
    /// <summary>
    /// Configures services in the container by calling the <see cref="IEngineServiceLoader.ConfigureServices"/> method for each discovered loader.
    /// This method is typically called after services have been added to the container.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer ConfigureServices();
    /// <summary>
    /// Finds and loads all available <see cref="IEngineServiceLoader"/> implementations from the assemblies in the current application domain.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer FindServiceLoaders();
}
