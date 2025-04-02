using Reoria.Engine.Common.Security.Encryption.Interfaces;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reoria.Engine.Common.Security.Encryption;

public class AesEncryptionService : Disposable, IEncryptionService
{
    public byte[] AesKey { get; protected set; }
    public byte[] AesIV { get; protected set; }

    public AesEncryptionService()
    {
        using Aes aes = Aes.Create();

        this.AesKey = aes.Key;
        this.AesIV = aes.IV;
    }

    public virtual void ChangeKey(byte[] key,  byte[] iv)
    {
        this.AesKey = key;
        this.AesIV = iv;
    }

    public byte[] Encrypt(byte[] rawData)
    {
        using Aes aes = Aes.Create();
        aes.Key = this.AesKey;
        aes.IV = this.AesIV;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

        cs.Write(rawData, 0, rawData.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    public byte[] Decrypt(byte[] encryptedData)
    {
        using Aes aes = Aes.Create();
        aes.Key = this.AesKey;
        aes.IV = this.AesIV;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write); // Set to write mode

        // Write the encrypted data to the CryptoStream, which will decrypt it
        cs.Write(encryptedData, 0, encryptedData.Length);
        cs.FlushFinalBlock(); // Ensure the final block is processed

        return ms.ToArray(); // Return the decrypted data from MemoryStream
    }
}
