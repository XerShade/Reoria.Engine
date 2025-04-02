namespace Reoria.Engine.Common.Security.Encryption.Interfaces;

public interface IEncryptionService : IDisposable, IAsyncDisposable
{
    byte[] AesKey { get; }
    byte[] AesIV { get; }

    void ChangeKey(byte[] key, byte[] iv);
    byte[] Decrypt(byte[] encryptedData);
    byte[] Encrypt(byte[] rawData);
}
