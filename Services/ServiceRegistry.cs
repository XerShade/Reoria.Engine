using Microsoft.Extensions.Logging;
using Reoria.Engine.Services.Interfaces;
using System.Collections.Concurrent;

namespace Reoria.Engine.Services;

public class ServiceRegistry(ILogger<IServiceRegistry> logger) : IServiceRegistry
{
    protected readonly ConcurrentDictionary<Type, object> services = [];
    protected readonly ILogger<IServiceRegistry> logger = logger;
    protected readonly Lock @lock = new();

    public virtual void RegisterService<TService>(TService instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        lock (this.@lock)
        {
            if (!this.services.ContainsKey(typeof(TService)))
            {
                this.services[typeof(TService)] = instance;
                this.logger.LogDebug("Added new registered instance of service '{service}' to the service registry.", typeof(TService).Name);
            }
        }
    }

    public virtual void UpdateService<TService>(TService instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        lock (this.@lock)
        {
            this.services[typeof(TService)] = instance;
            this.logger.LogDebug("Updated registered instance of service '{service}' in the service registry.", typeof(TService).Name);
        }
    }

    public virtual void RemoveService<TService>()
    {
        lock (this.@lock)
        {
            if (this.services.ContainsKey(typeof(TService)))
            {
                _ = this.services.Remove(typeof(TService), out _);
                this.logger.LogDebug("Removed registered instance of service '{service}' from the service registry.", typeof(TService).Name);
            }
        }
    }

    public virtual TService FetchService<TService>()
    {
        lock (this.@lock)
        {
            if (this.services.TryGetValue(typeof(TService), out object? service))
            {
                return (TService)service;
            }
            throw new InvalidOperationException($"No registered instance for service '{typeof(TService).Name}' not found.");
        }
    }
}
