using System.CodeDom.Compiler;
using System.Security.Cryptography;
using System.Text;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
{
    //Could have been a service, but i decided that as this isn't the first class that called from the GUI layer.
    public class Hasher
    {
        /// <summary>
        ///     Creates a hash from hmac type, key and text
        /// </summary>
        /// <param name="hmac">The hmac type used to hash</param>
        /// <param name="key">The key used create a hash from</param>
        /// <param name="text">The text used to create a hash from</param>
        /// <returns>The computed hash in base64</returns>
        public string Hash(HMAC hmac, byte[] key, string text)
        {
            hmac.Key = key;
            using var hasher = hmac;
            return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
        /// <summary>
        ///     Create a hash mased on a hash algorithm 
        /// </summary>
        /// <param name="hashAlgorithm">The hashAlgorithm used for the hash</param>
        /// <param name="text">The text the hash is based on</param>
        /// <param name="hashIterations">The times the hash algorithm should run</param>
        /// <returns>
        ///     Returns a hash in base64
        /// </returns>
        public string Hash(HashAlgorithm hashAlgorithm, string text, int hashIterations = 1)
        {
            using var hasher = hashAlgorithm;
            byte[] tempHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
            for (int i = 0; i < hashIterations - 1; i++)
            {
                tempHash = hasher.ComputeHash(tempHash);
            }

            return Convert.ToBase64String(tempHash);
        }
    }
}
