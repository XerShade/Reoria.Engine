using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Base.Container.Services;

public class ContainerServiceDefinitions(ILogger<ContainerServiceDefinitions> logger)
{
    protected readonly Lock @lock = new();
    protected readonly List<ContainerServiceDefinition> definitions = [];
    protected readonly ILogger<ContainerServiceDefinitions> logger = logger;

    public bool Any()
    {
        lock (this.@lock)
        {
            return this.definitions.Count != 0;
        }
    }

    public int Count()
    {
        lock (this.@lock)
        {
            return this.definitions.Count;
        }
    }

    public ContainerServiceDefinition[] GetSources()
    {
        lock (this.@lock)
        {
            return [.. this.definitions];
        }
    }

    public virtual void Dispose()
    {
        lock (this.@lock)
        {
            this.definitions.Clear();
        }
    }

    protected virtual void AddDefinition(ServiceLifetime lifetime, Type service, Type implementation)
    {
        lock (this.@lock)
        {
            bool source = (from d in this.definitions
                           where d.Service.Equals(service)
                           select d).Any();

            if (!source)
            {
                this.definitions.Add(new(lifetime, service, implementation));
                this.logger.LogDebug("Added service definition for '{Service}' using '{Implementation}' with lifetime '{Lifetime}'.",
                    service.Name, implementation.Name, lifetime.ToString());
            }
        }
    }

    protected virtual void AddDefinition(ServiceLifetime lifetime, Type service, object? instance)
    {
        lock (this.@lock)
        {
            bool source = (from d in this.definitions
                           where d.Service.Equals(service)
                           select d).Any();

            if (!source)
            {
                this.definitions.Add(new(lifetime, service, instance));
                this.logger.LogDebug("Added service definition for '{Service}' using existing instance with lifetime '{Lifetime}'.",
                    service.Name, lifetime.ToString());
            }
        }
    }

    protected virtual void RemoveDefinition(Type service)
    {
        lock (this.@lock)
        {
            ContainerServiceDefinition source = (from d in this.definitions
                                                 where d.Service.Equals(service)
                                                 select d).FirstOrDefault();

            if (this.definitions.Remove(source))
            {
                this.logger.LogDebug("Removed service definition for '{Service}'.", service.Name);
            }
        }
    }

    public virtual void AddScoped<TService>() =>
        this.AddDefinition(ServiceLifetime.Scoped, typeof(TService), typeof(TService));
    public virtual void AddScoped<TService>(TService instance) =>
        this.AddDefinition(ServiceLifetime.Scoped, typeof(TService), instance);
    public virtual void AddScoped<TService, TImplementation>()
        where TImplementation : class, TService =>
        this.AddDefinition(ServiceLifetime.Scoped, typeof(TService), typeof(TImplementation));

    public virtual void AddTransient<TService>() =>
        this.AddDefinition(ServiceLifetime.Transient, typeof(TService), typeof(TService));
    public virtual void AddTransient<TService, TImplementation>()
        where TImplementation : class, TService =>
        this.AddDefinition(ServiceLifetime.Transient, typeof(TService), typeof(TImplementation));

    public virtual void AddSingleton<TService>() =>
        this.AddDefinition(ServiceLifetime.Singleton, typeof(TService), typeof(TService));
    public virtual void AddSingleton<TService, TImplementation>()
        where TImplementation : class, TService =>
        this.AddDefinition(ServiceLifetime.Singleton, typeof(TService), typeof(TImplementation));

    public virtual void Remove<TService>() => this.RemoveDefinition(typeof(TService));
}
