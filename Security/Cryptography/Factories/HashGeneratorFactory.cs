using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Security.Cryptography.Factories.Interfaces;

namespace Reoria.Engine.Security.Cryptography.Factories;

/// <summary>
/// Factory for creating instances of <see cref="HashGenerator"/>.
/// This factory is responsible for managing the creation of hash generators using dependency injection.
/// </summary>
public class HashGeneratorFactory : IHashGeneratorFactory
{
    // The service provider used for dependency injection to resolve instances
    protected static IServiceProvider? ServiceProvider;

    /// <summary>
    /// Assigns a service provider to the factory. This allows the factory to resolve dependencies.
    /// </summary>
    /// <param name="services">The service provider to be assigned to the factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided service provider is null.</exception>
    public void AssignServiceProvider(IServiceProvider services)
    {
        // Check if the service provider is null and throw exception if so
        ArgumentNullException.ThrowIfNull(services);

        // Assign the provided service provider to the static field
        HashGeneratorFactory.ServiceProvider = services;
    }

    /// <summary>
    /// Creates a new instance of <see cref="HashGenerator"/> using the assigned service provider.
    /// </summary>
    /// <returns>A new instance of <see cref="HashGenerator"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is not assigned.</exception>
    public static HashGenerator Create()
    {
        // Ensure the service provider is assigned before trying to resolve services
        ArgumentNullException.ThrowIfNull(HashGeneratorFactory.ServiceProvider);

        // Resolve and return the HashGenerator instance from the service provider
        return HashGeneratorFactory.ServiceProvider.GetRequiredService<HashGenerator>();
    }
}
