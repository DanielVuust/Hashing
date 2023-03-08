using FluentResults;
using System;
using System.IO;
using System.Security.Cryptography;

namespace BlazorGuiServer.Data.Services.Helpers
{
    public class Decrypter
    {
        public Result<string> Decrypt(SymmetricAlgorithm algorithm, byte[] message, byte[] key, byte[] iv)
        {
            using (var algo = algorithm)
            {
                try
                {
                    algo.Padding = PaddingMode.PKCS7;
                    algo.Key = key;
                    algo.IV = iv;
                }
                catch (Exception ex)
                {

                }
                ICryptoTransform decryptor = algo.CreateDecryptor(algo.Key, algo.IV);

                using (MemoryStream memoryStream = new MemoryStream(message))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            try
                            {
                                return streamReader.ReadToEnd();

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }

        }
    }
}
