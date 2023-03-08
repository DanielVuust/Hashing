using BlazorGuiServer.Data.Services.Helpers;
using BlazorGuiServer.Data.Services.Managers;
using System.Security.Cryptography;

namespace BlazorGuiServer.Data.Services
{
    public class HashManagerService
    {
        public string GenerateHash(string hmacName, byte[] key, string text)
        {
            HmacManager macManager = new HmacManager();
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
            HmacManager macManager = new HmacManager();
            return macManager.GetSupportedHmacs();
        }
    }
}
