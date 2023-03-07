using System.Security.Cryptography;
using System.Text;

namespace Hashing
{
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
        public string Hash(HashAlgorithm hash, string text)
        {
            using var hasher = hash;
            return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
    }
}
