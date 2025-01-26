using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Security.Cryptography;

/// <summary>
/// Holds the configuration settings for the hash generator.
/// These settings define the cryptographic algorithm, namespace, assembly, and other relevant parameters.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HashGeneratorConfiguration"/> struct.
/// Retrieves configuration values from the provided <see cref="IConfiguration"/>.
/// </remarks>
/// <param name="configuration">The configuration object that provides the settings.</param>
public readonly struct HashGeneratorConfiguration(IConfiguration configuration)
{
    /// <summary>
    /// Defines the default cryptographic algorithm to be used.
    /// </summary>
    public const string DEFAULT_ALGORITHM = "SHA-256";
    /// <summary>
    /// Defines the default namespace in which the cryptographic algorithm class resides.
    /// </summary>
    public const string DEFAULT_ALGORITHM_NAMESPACE = "System.Security.Cryptography";
    /// <summary>
    /// Defines the default assembly name where the cryptographic algorithm class is found.
    /// </summary>
    public const string DEFAULT_ALGORITHM_ASSEMBLY = "System.Security.Cryptography.Algorithms";

    /// <summary>
    /// Gets the cryptographic algorithm to be used (e.g., SHA-256).
    /// </summary>
    public readonly string Algorithm = configuration.GetValue<string>("Security:Cryptography:Algorithm") ?? DEFAULT_ALGORITHM;

    /// <summary>
    /// Gets the namespace in which the cryptographic algorithm class resides.
    /// </summary>
    public readonly string AlgorithmNamespace = configuration.GetValue<string>("Security:Cryptography:AlgorithmNamespace") ?? DEFAULT_ALGORITHM_NAMESPACE;

    /// <summary>
    /// Gets the assembly name where the cryptographic algorithm class is found.
    /// </summary>
    public readonly string AlgorithmAssembly = configuration.GetValue<string>("Security:Cryptography:AlgorithmAssembly") ?? DEFAULT_ALGORITHM_ASSEMBLY;

    /// <summary>
    /// Gets the number of iterations used in the cryptographic operation.
    /// </summary>
    public readonly int Iterations = configuration.GetValue<int>("Security:Cryptography:Iterations");
}
