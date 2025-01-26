using Microsoft.Extensions.Configuration;
using Reoria.Engine.Security.Cryptography.Interfaces;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Reoria.Engine.Security.Cryptography;

/// <summary>
/// Provides functionality for generating cryptographic hash values.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HashGenerator"/> class.
/// </remarks>
/// <param name="configuration">Configuration object containing hash algorithm settings.</param>
public class HashGenerator(IConfiguration configuration) : IHashGenerator
{
    // Configuration object to retrieve hash algorithm settings
    protected readonly HashGeneratorConfiguration configuration = new(configuration);

    /// <summary>
    /// Generates a hash value for the given input and salt.
    /// </summary>
    /// <param name="input">The input string to be hashed.</param>
    /// <param name="salt">The salt to be combined with the input before hashing.</param>
    /// <returns>The base64 encoded hash value.</returns>
    /// <exception cref="ArgumentException">Thrown if the input is null or whitespace.</exception>
    /// <exception cref="FormatException">Thrown if the salt is not in a valid base64 format.</exception>
    public virtual string GenerateHash(string input, string salt)
    {
        // Check if input is null or whitespace
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"'{nameof(input)}' cannot be null or whitespace.", nameof(input));
        }

        // Create a HashAlgorithm instance based on the configuration
        using HashAlgorithm hashAlgorithmInstance = this.CreateHashAlgorithm(this.configuration.Algorithm, this.configuration.AlgorithmNamespace, this.configuration.AlgorithmAssembly);

        // Convert input and salt to byte arrays
        byte[] passwordBytes = Encoding.UTF8.GetBytes(input);
        byte[] saltBytes = Convert.FromBase64String(salt);

        // Combine the password and salt byte arrays
        byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, passwordWithSaltBytes, passwordBytes.Length, saltBytes.Length);

        // Compute the hash of the combined byte array
        byte[] hashBytes = hashAlgorithmInstance.ComputeHash(passwordWithSaltBytes);

        // Return the hash as a base64 string
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Creates a hash algorithm instance based on the specified algorithm name and assembly.
    /// </summary>
    /// <param name="algorithm">The name of the hash algorithm to be created.</param>
    /// <param name="algorithmNamespace">The namespace of the hash algorithm class.</param>
    /// <param name="assemblyName">The assembly containing the hash algorithm class.</param>
    /// <returns>An instance of the specified hash algorithm.</returns>
    /// <exception cref="NullReferenceException">Thrown if the algorithm type cannot be found or is not a valid hash algorithm.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the algorithm type does not implement the <see cref="HashAlgorithm"/> class.</exception>
    protected virtual HashAlgorithm CreateHashAlgorithm(string algorithm, string algorithmNamespace = "System.Security.Cryptography", string assemblyName = "System.Security.Cryptography.Algorithms")
    {
        // Error message for unsupported algorithms
        string errorMessage = $"The specified hash algorithm '{algorithm}' is not supported.";

        // Build the full class name
        string className = $"{algorithmNamespace}.{algorithm}, {assemblyName}";

        // Get the Type of the specified hash algorithm class
        Type algorithmType = Type.GetType(className) ?? throw new NullReferenceException(errorMessage);

        // Ensure the class is a valid hash algorithm
        if (algorithmType != null && typeof(HashAlgorithm).IsAssignableFrom(algorithmType))
        {
            // Get the static methods of the hash algorithm class
            MethodInfo[] createMethods = algorithmType.GetMethods(BindingFlags.Static | BindingFlags.Public);

            // Find the method that creates an instance of the algorithm (with no parameters)
            MethodInfo? createMethod = createMethods.FirstOrDefault(m => m.GetParameters().Length == 0);

            if (createMethod != null)
            {
                // Invoke the create method and return the hash algorithm instance
                return createMethod.Invoke(null, null) as HashAlgorithm ?? throw new NullReferenceException(errorMessage);
            }

            // If no static create method is found, try creating an instance using the constructor
            return Activator.CreateInstance(algorithmType) as HashAlgorithm ?? throw new NullReferenceException(errorMessage);
        }
        else
        {
            // Throw an exception if the algorithm type is invalid
            throw new InvalidOperationException(errorMessage);
        }
    }
}
