using FluentResults;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace BlazorGuiServer.Data.Services
{
    public class RsaSenderService
    {
        public Result EncryptUsingRsaPublicKey(byte[] rsaPublicKey)
        {

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(Convert.ToBase64String(rsaPublicKey));
            }

            return Result.Ok();
        }
    }
}
