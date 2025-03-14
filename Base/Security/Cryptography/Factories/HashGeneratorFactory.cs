using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Base.Security.Cryptography.Factories;

/// <summary>
/// Factory for creating instances of <see cref="HashGenerator"/>.
/// This factory is responsible for managing the creation of hash generators using dependency injection.
/// </summary>
[Container]
public static class HashGeneratorFactory
{
    #region HashGeneratorFactory: Service Definitions
    /// <summary>
    /// This method is invoked by <see cref="IEngineContainer"/> as part of the process of constructing its <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/> instance created by <see cref="IEngineContainer"/> to resolve dependencies for the application.</param>
    [ContainerAttribute.BuildServiceProvider]
    public static void BuildServiceProvider(IServiceProvider provider) =>
        // Assign the provided service provider to the static property of the HashGeneratorFactory class
        HashGeneratorFactory.ServiceProvider = provider;
    #endregion

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
}
