using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HashingDomain.Model;

namespace HashingDomain
{
    public class NewUserHelper
    {
        private readonly SecurePasswordDbContext _context;

        public NewUserHelper(SecurePasswordDbContext context)
        {
            this._context = context;
        }
        public User CreateNewUser(string username, string hash, string email, string salt, int hashIterations = 1)
        {
            User user = new User();
            user.Username = username;
            user.Hash = hash;
            user.Salt = salt;
            user.Email = email;
            user.HashIterations = hashIterations;

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
