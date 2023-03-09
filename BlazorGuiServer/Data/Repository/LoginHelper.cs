using BlazorGuiServer.Data.Repository.Model;
using FluentResults;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Repository
{
    public class LoginHelper
    {
        private readonly SecurePasswordDbContext _context;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<LoginHelper> _logger;

        public LoginHelper(SecurePasswordDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<LoginHelper>();
        }

        public Result<User?> GetUser(string username)
        {
            _logger.LogDebug("Calling GetUser");
            try
            {
                return Result.Ok(
                    _context.Users.FirstOrDefault(x => x.Username == username));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Result.Fail("an error occurred while getting user from db");
            }
            
        }
    }
}
