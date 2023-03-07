using HashingDomain;
using HashingDomain.Model;

namespace BlazorGuiServer.Data
{
    public class LoginHelper
    {
        private readonly SecurePasswordDbContext _context;

        public LoginHelper(SecurePasswordDbContext context)
        {
            _context = context;
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }
    }
}
