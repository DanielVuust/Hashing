using BlazorGuiServer.Data.Services.Helpers;
using BlazorGuiServer.Data.Services.Managers;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class SymmetricAlgorithmsManagerService
    {
        public List<string> GetSupportedSymmetricAlgorithms()
        {
            SymmetricAlgorithmManager symmetricAlgorithmManager = new SymmetricAlgorithmManager();
            return symmetricAlgorithmManager.GetSupportedAlgorithms();
        }

        public Result<byte[]> EncryptMessage(string message, byte[] key, byte[] iv, string selectedEncryption)
        {
            SymmetricAlgorithmManager manager = new SymmetricAlgorithmManager();
            var algo = manager.SelectSymmetricAlgorithmManager(selectedEncryption);

            Encrypter encrypter = new Encrypter();

            Result<byte[]> encryptedText = encrypter.Encrypt(algo, message, key, iv);
            return Result.Ok(encryptedText.Value);
        }
        public Result<string> DecryptEncryptedMessage(byte[] encryptedMessage, byte[] key, byte[] iv, string selectedEncryption)
        {

            SymmetricAlgorithmManager manager = new SymmetricAlgorithmManager();
            var algo = manager.SelectSymmetricAlgorithmManager(selectedEncryption);

            Decrypter decrypter = new Decrypter();

            Result<string> decryptedText = decrypter.Decrypt(algo, encryptedMessage, key, iv);
            return decryptedText;
        }
    }
}
