namespace Reoria.Engine.Security.Cryptography.Factories.Interfaces;

/// <summary>
/// Interface for a factory responsible for creating instances of <see cref="HashGenerator"/>.
/// The factory allows for managing the lifecycle of <see cref="HashGenerator"/> instances using dependency injection.
/// </summary>
public interface IHashGeneratorFactory
{
    /// <summary>
    /// Assigns a service provider to the factory. This enables the factory to resolve dependencies when creating <see cref="HashGenerator"/> instances.
    /// </summary>
    /// <param name="services">The service provider to be assigned for dependency injection resolution.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided service provider is null.</exception>
    void AssignServiceProvider(IServiceProvider services);
}
