using BlazorGuiServer.Data.Repository.Dto;
using BlazorGuiServer.Data.Services.Helpers;
using BlazorGuiServer.Data.Services.Managers;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class SymmetricAlgorithmsManagerService
    {
        public List<string> GetSupportedSymmetricAlgorithms()
        {
            SymmetricAlgorithmManager symmetricAlgorithmManager = new();
            return symmetricAlgorithmManager.GetSupportedAlgorithms();
        }

        public Result<EncryptedMessageDto> EncryptMessage(string message, string selectedEncryption)
        {
            SymmetricAlgorithmManager manager = new SymmetricAlgorithmManager();
            var algo = manager.SelectSymmetricAlgorithmManager(selectedEncryption);

            algo.GenerateIV();
            algo.GenerateKey();

            EncryptedMessageDto dto = new EncryptedMessageDto
            {
                EncryptionKey = algo.Key,
                EncryptionIv = algo.IV
            };

            Encrypter encrypter = new Encrypter();

            Result<byte[]> encryptedText = encrypter.Encrypt(algo, message);
            dto.EncryptedMessage = encryptedText.Value;
            return Result.Ok(dto);
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
