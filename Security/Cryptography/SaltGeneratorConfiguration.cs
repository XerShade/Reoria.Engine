using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Security.Cryptography;

/// <summary>
/// Represents the configuration for the salt generator, including parameters like
/// the salt length, which is used in cryptographic operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SaltGeneratorConfiguration"/> struct.
/// The constructor reads the salt length from the provided <see cref="IConfiguration"/>.
/// </remarks>
/// <param name="configuration">The <see cref="IConfiguration"/> instance used to retrieve configuration settings.</param>
public readonly struct SaltGeneratorConfiguration(IConfiguration configuration)
{
    /// <summary>
    /// Gets the length of the salt to be generated, as specified in the configuration.
    /// </summary>
    public readonly int SaltLength = configuration.GetValue<int>("Security:Cryptography:SaltLength");
}
