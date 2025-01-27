using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Security.Cryptography.Factories.Interfaces;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Security.Cryptography.Factories;

/// <summary>
/// A factory class responsible for creating instances of <see cref="SaltGenerator"/> 
/// using a service provider to resolve dependencies.
/// </summary>
public class SaltGeneratorFactory : ISaltGeneratorFactory
{
    /// <summary>
    /// The <see cref="IServiceProvider"/> used to resolve dependencies for creating <see cref="SaltGenerator"/> instances.
    /// </summary>
    protected static IServiceProvider? ServiceProvider;

    /// <summary>
    /// Assigns the service provider to the factory.
    /// This method allows the factory to resolve services for creating a <see cref="SaltGenerator"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceProvider"/> that provides access to registered services.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/> is null.</exception>
    public void AssignServiceProvider(IServiceProvider services)
    {
        // Throws an exception if the provided service provider is null.
        ArgumentNullException.ThrowIfNull(services);

        // Sets the static ServiceProvider field to the provided service provider.
        SaltGeneratorFactory.ServiceProvider = services;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SaltGenerator"/> using the assigned <see cref="IServiceProvider"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="SaltGenerator"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is not assigned.</exception>
    public static ISaltGenerator Create()
    {
        // Throws an exception if the ServiceProvider is not assigned.
        ArgumentNullException.ThrowIfNull(SaltGeneratorFactory.ServiceProvider);

        // Resolves and returns a new instance of <see cref="SaltGenerator"/> from the ServiceProvider.
        return SaltGeneratorFactory.ServiceProvider.GetRequiredService<ISaltGenerator>();
    }
}
