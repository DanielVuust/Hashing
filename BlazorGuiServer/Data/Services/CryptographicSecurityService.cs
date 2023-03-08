using BlazorGuiServer.Data.Services.Helpers;
using System.Security.Cryptography;
using BlazorGuiServer.Data.Services.Managers;

namespace BlazorGuiServer.Data.Services
{
    public class CryptographicSecurityService
    {
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
