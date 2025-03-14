using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Base.Security.Cryptography.Factories;

/// <summary>
/// A factory class responsible for creating instances of <see cref="SaltGenerator"/> 
/// using a service provider to resolve dependencies.
/// </summary>
[Container]
public static class SaltGeneratorFactory
{
    #region SaltGeneratorFactory: Service Definitions
    /// <summary>
    /// This method is invoked by <see cref="IEngineContainer"/> as part of the process of constructing its <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/> instance created by <see cref="IEngineContainer"/> to resolve dependencies for the application.</param>
    [ContainerAttribute.BuildServiceProvider]
    public static void BuildServiceProvider(IServiceProvider provider) =>
        // Assign the provided service provider to the static property of the SaltGeneratorFactory class
        SaltGeneratorFactory.ServiceProvider = provider;
    #endregion

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
}
