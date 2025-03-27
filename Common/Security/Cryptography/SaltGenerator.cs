using Microsoft.Extensions.Configuration;
using Reoria.Engine.Common;
using Reoria.Engine.Security.Cryptography.Interfaces;
using System.Security.Cryptography;

namespace Reoria.Engine.Security.Cryptography;

/// <summary>
/// Generates cryptographic salts used for operations such as password hashing.
/// The salt length is configured via <see cref="SaltGeneratorConfiguration"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SaltGenerator"/> class using the provided configuration.
/// </remarks>
/// <param name="configuration">The <see cref="IConfiguration"/> instance used to retrieve salt generator settings.</param>
public class SaltGenerator(IConfiguration configuration) : Disposable(), ISaltGenerator
{
    /// <summary>
    /// The configuration settings for the salt generator, including salt length.
    /// </summary>
    protected readonly SaltGeneratorConfiguration configuration = new(configuration);

    /// <summary>
    /// Generates a cryptographic salt as a base64-encoded string.
    /// The salt is generated using a secure random number generator and the configured salt length.
    /// </summary>
    /// <returns>A base64-encoded string representing the generated salt.</returns>
    public virtual string GenerateSalt()
    {
        // Generate a secure random byte array of the configured length
        byte[] saltBytes = RandomNumberGenerator.GetBytes(this.configuration.SaltLength);

        // Convert the byte array to a base64 string and return it
        return Convert.ToBase64String(saltBytes);
    }
}
