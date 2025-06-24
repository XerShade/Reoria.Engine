using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container.Services.Interfaces;

namespace Reoria.Engine.Container.Registrars;

/// <summary>
/// Defines a contract for registering services to the application's dependency injection container.
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// Registers application services into the given <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="registryGuard">The service registry guard to register services into.</param>
    void RegisterServices(IServiceRegistryGuard registryGuard);
}