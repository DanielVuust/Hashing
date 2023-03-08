using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlazorGuiServer.Data.Repository.Model;
using FluentResults;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Repository
{
    public class NewUserHelper
    {
        private readonly SecurePasswordDbContext _context;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<NewUserHelper> _logger;


        public NewUserHelper(SecurePasswordDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<NewUserHelper>();

        }
        public Result<User> CreateNewUser(string username, string hash, string email, string salt, int hashIterations = 1)
        {
            _logger.LogDebug("Calling CreateNewUser");

            User user = new()
            {
                Username = username,
                Hash = hash,
                Salt = salt,
                Email = email,
            };

            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not save new user to db. ex: {ex}");
                return Result.Fail<User>(new Error("Could not save new user to db").CausedBy(ex));
            }

            return Result.Ok(user);
        }
    }
}
