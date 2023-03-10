using System.Security.Cryptography;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class RsaReceiverService
    {
        public Result<RSA?> GenerateRasKeyAndSaveInCsp(string containerName)
        {
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };
            try
            {
                return Result.Ok((RSA)new RSACryptoServiceProvider(2048, parameters));
            }
            catch (Exception ex)
            {
                //TODO Add logging.
                return Result.Fail(ex.ToString());
            }
        }
        public Result<byte[]> DecryptEncryptedString(byte[] encryptedText, string containerName)
        {
            var parameters = new CspParameters
            {
                KeyContainerName = containerName,
            };

            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048,parameters);
                return rsa.Decrypt(encryptedText, false);
            }
            catch (Exception ex)
            {
                //TODO Add logging.
                return Result.Fail(ex.ToString());
            }
        }

        public Result ClearKeyFromCsp(string containerName)
        {
            var parameters = new CspParameters
            {
                KeyContainerName = containerName
            };

            try
            {
                using var rsa = new RSACryptoServiceProvider(parameters)
                {
                    PersistKeyInCsp = false
                };
                rsa.Clear();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.ToString());
            }
            return Result.Ok();
        }
    }
}
