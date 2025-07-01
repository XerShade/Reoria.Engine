using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Container.Registrars;

/// <summary>
/// Defines a contract for registering services to the application's dependency injection container.
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// Registers services to the application's dependency injection container with the provided Autofac container builder.
    /// </summary>
    /// <param name="builder">The Autofac <see cref="ContainerBuilder"/> to register services with.</param>
    /// <param name="configuration">The application's <see cref="IConfiguration"/>.</param>
    /// <param name="loggerFactory">The application's <see cref="ILoggerFactory"/>.</param>
    void RegisterServices(ContainerBuilder builder, IConfiguration configuration, ILoggerFactory loggerFactory);
}