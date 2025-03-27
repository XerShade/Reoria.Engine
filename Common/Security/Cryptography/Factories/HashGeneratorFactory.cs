using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Security.Cryptography.Interfaces;
using Reoria.Engine.Security.Cryptography;

namespace Reoria.Engine.Common.Security.Cryptography.Factories;

/// <summary>
/// Factory for creating instances of <see cref="HashGenerator"/>.
/// This factory is responsible for managing the creation of hash generators using dependency injection.
/// </summary>
public static class HashGeneratorFactory
{
    // The service provider used for dependency injection to resolve instances
    private static IServiceProvider? ServiceProvider;

    /// <summary>
    /// Creates a new instance of <see cref="HashGenerator"/> using the assigned service provider.
    /// </summary>
    /// <returns>A new instance of <see cref="HashGenerator"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is not assigned.</exception>
    public static IHashGenerator Create()
    {
        // Ensure the service provider is assigned before trying to resolve services
        ArgumentNullException.ThrowIfNull(HashGeneratorFactory.ServiceProvider);

        // Resolve and return the HashGenerator instance from the service provider
        return HashGeneratorFactory.ServiceProvider.GetRequiredService<IHashGenerator>();
    }

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> used to resolve dependencies for creating <see cref="HashGenerator"/> instances.
    /// </summary>
    /// <param name="provider"></param>
    public static void SetServiceProvider(IServiceProvider provider)
    {
        // Throws an exception if the service provider is null.
        ArgumentNullException.ThrowIfNull(provider);

        // Store the service provider for later use.
        HashGeneratorFactory.ServiceProvider = provider;
    }
}
