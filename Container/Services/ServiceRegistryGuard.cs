using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Services.Interfaces;

namespace Reoria.Engine.Container.Services;

/// <summary>
/// Default implementation of <see cref="IServiceRegistryGuard"/>.
/// Provides safe registration of services, preventing accidental duplicates.
/// </summary>
public class ServiceRegistryGuard(ILogger<IServiceRegistryGuard> logger, IServiceCollection services) : IServiceRegistryGuard
{
    /// <summary>
    /// Logger for diagnostic and warning messages.
    /// </summary>
    protected readonly ILogger<IServiceRegistryGuard> Logger = logger;
    /// <summary>
    /// The <see cref="IServiceCollection"/> to perform operations on.
    /// </summary>
    protected readonly IServiceCollection Services = services;

    /// <summary>
    /// Tracks registered service types and their corresponding implementations.
    /// </summary>
    protected readonly Dictionary<Type, Type> RegisteredServices = new() {
        { typeof(IConfiguration), typeof(IConfiguration) },
        { typeof(ILoggerFactory), typeof(ILoggerFactory) },
        { typeof(ILogger<>), typeof(Logger<>) },
    };

    /// <summary>
    /// Attempts to register a service with the specified implementation type and lifetime,
    /// but only if the service has not already been registered.
    /// </summary>
    /// <inheritdoc/>
    public void TryRegister<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class where TImplementation : class, TService
    {
        Type serviceType = typeof(TService);
        Type implType = typeof(TImplementation);

        if (this.RegisteredServices.TryGetValue(serviceType, out Type? value))
        {
            this.Logger.LogWarning(
                "Service '{Implementation}' already registered with '{Service}', cannot register '{NewService}'.",
                implType.Name, value.Name, serviceType.Name);
            return;
        }

        if (this.RegisteredServices.TryAdd(serviceType, implType))
        {
            _ = lifetime switch
            {
                ServiceLifetime.Singleton => this.Services.AddSingleton<TService, TImplementation>(),
                ServiceLifetime.Scoped => this.Services.AddScoped<TService, TImplementation>(),
                ServiceLifetime.Transient => this.Services.AddTransient<TService, TImplementation>(),
                _ => throw new NotSupportedException()
            };
        }
    }

    /// <summary>
    /// Attempts to register a specific instance of a service,
    /// but only if the service has not already been registered.
    /// </summary>
    /// <inheritdoc/>
    public void TryRegister<TService, TImplementation>(TImplementation implementationInstance)
        where TService : class where TImplementation : class, TService
    {
        Type serviceType = typeof(TService);
        Type implType = typeof(TImplementation);

        if (this.RegisteredServices.TryGetValue(serviceType, out Type? value))
        {
            this.Logger.LogWarning(
                "Service '{Implementation}' already registered with '{Service}', cannot register '{NewService}'.",
                implType.Name, value.Name, serviceType.Name);
            return;
        }

        if (this.RegisteredServices.TryAdd(serviceType, implType))
        {
            _ = this.Services.AddSingleton<TService>(implementationInstance);
        }
    }

    /// <summary>
    /// Registers a service with the specified implementation type and lifetime.
    /// </summary>
    /// <inheritdoc/>
    public void Register<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class where TImplementation : class, TService
    {
        Type serviceType = typeof(TService);
        Type implType = typeof(TImplementation);

        _ = this.RegisteredServices.TryAdd(serviceType, implType);

        _ = lifetime switch
        {
            ServiceLifetime.Singleton => this.Services.AddSingleton<TService, TImplementation>(),
            ServiceLifetime.Scoped => this.Services.AddScoped<TService, TImplementation>(),
            ServiceLifetime.Transient => this.Services.AddTransient<TService, TImplementation>(),
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    /// Registers a specific instance of a service.
    /// </summary>
    /// <inheritdoc/>
    public void Register<TService, TImplementation>(TImplementation implementationInstance)
        where TService : class where TImplementation : class, TService
    {
        Type serviceType = typeof(TService);
        Type implType = typeof(TImplementation);

        _ = this.RegisteredServices.TryAdd(serviceType, implType);

        _ = this.Services.AddSingleton<TService>(implementationInstance);
    }
}
