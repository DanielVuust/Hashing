using Hashing;
using System.Security.Cryptography;

namespace BlazorGuiServer.Data.Management.Services
{
    public class CryptographicSecurityService
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
        public string CreateSalt()
        {
            return Convert.ToBase64String(new KeyManager().CreateKey());
        }
        public string CreateHashForPassword(string password, string salt)
        {
            Hasher hasher = new Hasher();
            return hasher.Hash(SHA256.Create(), password + salt, 20000);
        }
    }
}
