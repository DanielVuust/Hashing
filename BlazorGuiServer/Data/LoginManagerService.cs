using BlazorGuiServer.Data;
using Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingDomain
{
    internal class LoginManagerService
    {
        private readonly SecurePasswordDbContext _context;
        public LoginManagerService(SecurePasswordDbContext context)
        {
            _context = context;
        }

        public void CreateNewUser(string username, string password, string email)
        {
            string salt = Convert.ToBase64String(new KeyManager().CreateKey());
            Hasher hasher = new Hasher();

            var i = password + salt;
            string hash = hasher.Hash(SHA256.Create(), i);

            NewUserHelper helper = new NewUserHelper(_context);
            helper.CreateNewUser(username, hash, email, salt);

        }

        public bool TryLogin(string username, string password)
        {
            var user = new LoginHelper(_context).GetUser(username);
            if (user == null)
            {
                return false;
            }

            Hasher hasher = new Hasher();
            string hash = hasher.Hash(SHA256.Create(), password + user.Salt);

            if (hash != user.Hash)
            {
                return false;
            }
            return true;
        }
    }
}
