using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Container.Services.Interfaces;

/// <summary>
/// Provides guarded methods to register services in an <see cref="IServiceCollection"/>.
/// Ensures consistent and safe service registration logic.
/// </summary>
public interface IServiceRegistryGuard
{
    /// <summary>
    /// Registers a service with the specified implementation type and lifetime.
    /// </summary>
    /// <typeparam name="TService">The service interface type.</typeparam>
    /// <typeparam name="TImplementation">The concrete implementation type of the service.</typeparam>
    /// <param name="lifetime">The lifetime of the service (e.g., Singleton, Scoped, Transient).</param>
    void Register<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService;

    /// <summary>
    /// Registers a specific instance of a service.
    /// </summary>
    /// <typeparam name="TService">The service interface type.</typeparam>
    /// <typeparam name="TImplementation">The type of the instance implementing the service.</typeparam>
    /// <param name="implementationInstance">The instance to be registered.</param>
    void Register<TService, TImplementation>(TImplementation implementationInstance)
        where TService : class
        where TImplementation : class, TService;

    /// <summary>
    /// Attempts to register a service with the specified implementation type and lifetime,
    /// but only if the service has not already been registered.
    /// </summary>
    /// <typeparam name="TService">The service interface type.</typeparam>
    /// <typeparam name="TImplementation">The concrete implementation type of the service.</typeparam>
    /// <param name="lifetime">The lifetime of the service (e.g., Singleton, Scoped, Transient).</param>
    void TryRegister<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService;

    /// <summary>
    /// Attempts to register a specific instance of a service,
    /// but only if the service has not already been registered.
    /// </summary>
    /// <typeparam name="TService">The service interface type.</typeparam>
    /// <typeparam name="TImplementation">The type of the instance implementing the service.</typeparam>
    /// <param name="implementationInstance">The instance to be registered.</param>
    void TryRegister<TService, TImplementation>(TImplementation implementationInstance)
        where TService : class
        where TImplementation : class, TService;
}
