using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Base.Container.Services;

public readonly struct ContainerServiceDefinition
{
    public readonly Type Service;
    public readonly Type Implementation;
    public readonly ServiceLifetime Lifetime;

    public ContainerServiceDefinition(ServiceLifetime lifetime, Type service, Type implementation)
    {
        this.Service = service ?? throw new ArgumentNullException(nameof(service));
        this.Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        this.Lifetime = lifetime;
        this.Instance = null;
    }

    public readonly object? Instance = null;
    public ContainerServiceDefinition(ServiceLifetime lifetime, Type service, object? instance)
    {
        this.Service = service ?? throw new ArgumentNullException(nameof(service));
        this.Implementation = service;
        this.Lifetime = lifetime;
        this.Instance = instance;
    }
}