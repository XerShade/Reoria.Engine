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
    /// Registers services with the container by calling any static methods tagged with the <see cref="ServiceAttribute.RegisterServicesAttribute"/> attribute in the service.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer RegisterServices();
    /// <summary>
    /// Builds the <see cref="ServiceProvider"/> from the registered services. Once this method is called, no more services can be added to the container.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service collection is empty when attempting to build the provider.</exception>
    IEngineServiceContainer BuildServiceProvider();
    /// <summary>
    /// Configures any services registerd with the container by calling any static methods tagged with the <see cref="ServiceAttribute.ConfigureServicesAttribute"/> attribute in the service.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer ConfigureServices();
    /// <summary>
    /// Discovers all service classes that have been tagged with the <see cref="ServiceAttribute"/> attribute.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    IEngineServiceContainer DiscoverServices();
}
