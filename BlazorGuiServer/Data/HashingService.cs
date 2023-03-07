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
            Encryptor encryptor = new Encryptor();
            return encryptor.Encrypt(hmac, key, text);
        }

        public List<string> GetSupportedHmacs()
        {
            MacManager macManager = new MacManager();
            return macManager.GetSupportedHmacs();
        }
    }
}
