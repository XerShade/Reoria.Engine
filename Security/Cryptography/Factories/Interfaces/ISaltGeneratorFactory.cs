namespace Reoria.Engine.Security.Cryptography.Factories.Interfaces;

/// <summary>
/// Defines the contract for a salt generator factory that is responsible for creating
/// salt generators used in cryptographic operations, such as password hashing.
/// </summary>
public interface ISaltGeneratorFactory
{
    /// <summary>
    /// Assigns the service provider to the salt generator factory.
    /// This allows the factory to resolve dependencies needed for creating salt generators.
    /// </summary>
    /// <param name="services">The <see cref="IServiceProvider"/> that provides access to registered services.</param>
    void AssignServiceProvider(IServiceProvider services);
}
