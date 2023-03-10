using FluentResults;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;

namespace BlazorGuiServer.Data.Services
{
    public class RsaSenderService
    {
        public Result<byte[]> EncryptUsingRsaPublicKey(string rsaPublicKey, string textToEncrypt)
        {

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(rsaPublicKey);
                var bytes = rsa.Encrypt(Encoding.Unicode.GetBytes(textToEncrypt), false);
                return bytes;
            }

            return Result.Ok();
        }
    }
}
