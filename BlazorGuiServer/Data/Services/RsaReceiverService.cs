using System.Security.Cryptography;
using BlazorGuiServer.Data.Repository.Dto;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class RsaReceiverService
    {
        public Result<GeneratedRasKey> GenerateRasKey()
        {
            GeneratedRasKey key = new();
            using (var rsa = RSA.Create(2048))
            {
                key.PublicKey = rsa.ExportRSAPublicKey();
                key.PrivateKey = rsa.ExportRSAPrivateKey();
            }
            return key;
        }
    }
}
