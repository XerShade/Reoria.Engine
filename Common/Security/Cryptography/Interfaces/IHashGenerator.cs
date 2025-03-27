namespace Reoria.Engine.Security.Cryptography.Interfaces;

/// <summary>
/// Interface for generating cryptographic hashes.
/// Implementations of this interface should provide a method to generate a hash for a given input and salt.
/// </summary>
public interface IHashGenerator
{
    /// <summary>
    /// Generates a cryptographic hash for the provided input string, combined with a salt.
    /// </summary>
    /// <param name="input">The input string that needs to be hashed.</param>
    /// <param name="salt">The salt string to be combined with the input before hashing.</param>
    /// <returns>A base64-encoded string representing the hash of the input and salt.</returns>
    /// <exception cref="ArgumentException">Thrown if the input is null, empty, or contains only whitespace.</exception>
    string GenerateHash(string input, string salt);
}
