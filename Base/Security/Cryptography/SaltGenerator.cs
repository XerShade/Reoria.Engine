using Microsoft.Extensions.Configuration;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Services;
using Reoria.Engine.Base.Security.Cryptography.Interfaces;
using System.Security.Cryptography;

namespace Reoria.Engine.Base.Security.Cryptography;

/// <summary>
/// Generates cryptographic salts used for operations such as password hashing.
/// The salt length is configured via <see cref="SaltGeneratorConfiguration"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SaltGenerator"/> class using the provided configuration.
/// </remarks>
/// <param name="configuration">The <see cref="IConfiguration"/> instance used to retrieve salt generator settings.</param>
[Container]
public class SaltGenerator(IConfiguration configuration) : Disposable(), ISaltGenerator
{
    #region SaltGenerator: Service Definitions
    [ContainerAttribute.DiscoverSerivceDefinitions]
    public static void DiscoverSerivceDefinitions(ContainerServiceDefinitions services) =>
        services.AddScoped<ISaltGenerator, SaltGenerator>();
    #endregion

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
