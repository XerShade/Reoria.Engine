using Microsoft.Extensions.DependencyInjection;

namespace Reoria.Engine.Container.Registrars;

/// <summary>
/// Defines a contract for registering services to the application's dependency injection container.
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// Registers application services into the given <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    void RegisterServices(IServiceCollection services);
}