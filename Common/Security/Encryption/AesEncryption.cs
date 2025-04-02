using System.Security.Cryptography;

namespace Reoria.Engine.Common.Security.Encryption;

/// <summary>
/// A class for performing AES encryption and decryption operations using a specified key, IV, cipher mode, and padding mode.
/// </summary>
public class AesEncryption : Disposable, IDisposable
{
    /// <summary>
    /// The AES encryption key.
    /// </summary>
    public readonly byte[] Key;

    /// <summary>
    /// The initialization vector (IV) for AES encryption.
    /// </summary>
    public readonly byte[] IV;

    /// <summary>
    /// The cipher mode used in AES encryption.
    /// </summary>
    public readonly CipherMode CipherMode;

    /// <summary>
    /// The padding mode used in AES encryption.
    /// </summary>
    public readonly PaddingMode PaddingMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="AesEncryption"/> class using a randomly generated key and IV, with CBC cipher mode and PKCS7 padding.
    /// </summary>
    public AesEncryption()
    {
        lock (this.@lock)
        {
            using Aes aes = Aes.Create();

            this.Key = aes.Key;
            this.IV = aes.IV;
            this.CipherMode = CipherMode.CBC;
            this.PaddingMode = PaddingMode.PKCS7;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AesEncryption"/> class using the specified AES key and IV, with CBC cipher mode and PKCS7 padding.
    /// </summary>
    /// <param name="aesKey">The AES key to use for encryption and decryption.</param>
    /// <param name="aesIV">The initialization vector (IV) to use for encryption and decryption.</param>
    public AesEncryption(byte[] aesKey, byte[] aesIV)
    {
        lock (this.@lock)
        {
            this.Key = aesKey;
            this.IV = aesIV;
            this.CipherMode = CipherMode.CBC;
            this.PaddingMode = PaddingMode.PKCS7;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AesEncryption"/> class using the specified AES key, IV, cipher mode, and padding mode.
    /// </summary>
    /// <param name="aesKey">The AES key to use for encryption and decryption.</param>
    /// <param name="aesIV">The initialization vector (IV) to use for encryption and decryption.</param>
    /// <param name="cipherMode">The cipher mode to use for AES encryption.</param>
    /// <param name="paddingMode">The padding mode to use for AES encryption.</param>
    public AesEncryption(byte[] aesKey, byte[] aesIV, CipherMode cipherMode, PaddingMode paddingMode)
    {
        lock (this.@lock)
        {
            this.Key = aesKey;
            this.IV = aesIV;
            this.CipherMode = cipherMode;
            this.PaddingMode = paddingMode;
        }
    }

    /// <summary>
    /// Encrypts the provided data using the AES encryption algorithm with the current key, IV, cipher mode, and padding mode.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <returns>A byte array containing the encrypted data.</returns>
    public byte[] Encrypt(byte[] data)
    {
        lock (this.@lock)
        {
            using Aes aes = this.CreateAes();

            return this.ProcessEncryption(data, aes.CreateEncryptor(), CryptoStreamMode.Write);
        }
    }

    /// <summary>
    /// Decrypts the provided encrypted data using the AES decryption algorithm with the current key, IV, cipher mode, and padding mode.
    /// </summary>
    /// <param name="data">The encrypted data to decrypt.</param>
    /// <returns>A byte array containing the decrypted data.</returns>
    public byte[] Decrypt(byte[] data)
    {
        lock (this.@lock)
        {
            using Aes aes = this.CreateAes();

            return this.ProcessEncryption(data, aes.CreateDecryptor(), CryptoStreamMode.Write);
        }
    }

    /// <summary>
    /// Creates an instance of the AES algorithm with the current key, IV, cipher mode, and padding mode.
    /// </summary>
    /// <returns>An instance of the AES algorithm configured with the current settings.</returns>
    protected Aes CreateAes()
    {
        Aes aes = Aes.Create();

        aes.Key = this.Key;
        aes.IV = this.IV;
        aes.Mode = this.CipherMode;
        aes.Padding = this.PaddingMode;

        return aes;
    }

    /// <summary>
    /// Processes the encryption or decryption of the data using the specified crypto transform and stream mode.
    /// This method is used by both the <see cref="Encrypt"/> and <see cref="Decrypt"/> methods.
    /// </summary>
    /// <param name="data">The data to encrypt or decrypt.</param>
    /// <param name="cryptoTransform">The encryption or decryption transformation to apply.</param>
    /// <param name="cryptoStreamMode">The mode for the <see cref="CryptoStream"/> (Write or Read).</param>
    /// <returns>A byte array containing the processed data (either encrypted or decrypted).</returns>
    protected virtual byte[] ProcessEncryption(byte[] data, ICryptoTransform cryptoTransform, CryptoStreamMode cryptoStreamMode)
    {
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, cryptoTransform, cryptoStreamMode);

        cs.Write(data, 0, data.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }
}
