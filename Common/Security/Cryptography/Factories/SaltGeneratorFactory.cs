using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Common.Security.Cryptography.Factories;

/// <summary>
/// A factory class responsible for creating instances of <see cref="SaltGenerator"/> 
/// using a service provider to resolve dependencies.
/// </summary>
public static class SaltGeneratorFactory
{
    /// <summary>
    /// The <see cref="IServiceProvider"/> used to resolve dependencies for creating <see cref="SaltGenerator"/> instances.
    /// </summary>
    private static IServiceProvider? ServiceProvider;

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

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> used to resolve dependencies for creating <see cref="SaltGenerator"/> instances.
    /// </summary>
    /// <param name="provider"></param>
    public static void SetServiceProvider(IServiceProvider provider)
    {
        // Throws an exception if the service provider is null.
        ArgumentNullException.ThrowIfNull(provider);

        // Store the service provider for later use.
        SaltGeneratorFactory.ServiceProvider = provider;
    }
}
