namespace Reoria.Engine.Security.Cryptography.Interfaces;

/// <summary>
/// Defines the contract for a salt generator, which is responsible for generating
/// cryptographic salts used in operations like password hashing.
/// </summary>
public interface ISaltGenerator : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Generates a cryptographic salt as a string.
    /// The generated salt is typically used for securely hashing passwords or other sensitive data.
    /// </summary>
    /// <returns>A string representing the generated salt.</returns>
    string GenerateSalt();
}
