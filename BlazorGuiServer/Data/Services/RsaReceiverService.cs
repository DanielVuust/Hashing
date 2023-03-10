using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using BlazorGuiServer.Data.Repository.Dto;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class RsaReceiverService
    {
        public Result<string> GenerateRasKey()
        {
            GeneratedRasKey key = new();
            using (var rsa = RSA.Create(2048))
            {
                return rsa.ToXmlString(true);
                key.PrivateKey = rsa.ExportRSAPrivateKey();
            }
        }
        public Result<RSA> GenerateRasKey2()
        {
            return RSA.Create(2048);
        }
        public Result<byte[]> DecryptEncryptedString(byte[] encryptedText, string rsaXmlString)
        {
            try
            {
                RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider(2048);
                using (var rsa = rsa2)
                {
                    rsa.FromXmlString(rsaXmlString);
                    return rsa.Decrypt(encryptedText, false);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
