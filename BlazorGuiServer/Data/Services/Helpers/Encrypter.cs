using System;
using System.Security.Cryptography;
using FluentResults;

namespace BlazorGuiServer.Data.Services.Helpers
{
    public class Encrypter
    {
        public Result<byte[]> Encrypt(SymmetricAlgorithm algorithm, string message, byte[] key, byte[] iv)
        {
            byte[] array = new byte[64];
            using (var s = algorithm)
            {
                try
                {
                    s.Padding = PaddingMode.PKCS7;

                    s.Key = key;
                    s.IV = iv;
                }
                catch (Exception ex)
                {

                }
                ICryptoTransform encryptor = s.CreateEncryptor(s.Key, s.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(message);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            return array;
        }
    }
}
