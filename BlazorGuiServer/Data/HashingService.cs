using Hashing;
using System.Security.Cryptography;

namespace BlazorGuiServer.Data
{
    public class HashingService
    {

        public string GenerateHash(string hmacName, byte[] key, string text)
        {
            MacManager macManager = new MacManager();
            HMAC hmac = macManager.SelectHmac(hmacName);
            if (hmac == null || key == null || text == null)
            {
                return "error";
            }
            Hasher hasher = new Hasher();
            return hasher.Hash(hmac, key, text);
        }

        public List<string> GetSupportedHmacs()
        {
            MacManager macManager = new MacManager();
            return macManager.GetSupportedHmacs();
        }
    }
}
