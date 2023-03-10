using FluentResults;
using BlazorGuiServer.Data.Repository.Model;
using BlazorGuiServer.Data.Services.ServiceHelpers;

namespace BlazorGuiServer.Data.Services
{
    internal class LoginManagerService
    {
        private readonly SecurePasswordDbContext _context;
        private readonly ILoggerFactory _loggerFactory;
        private readonly CryptographicSecurityService _cryptographicSecurity;
        private readonly ILogger<LoginManagerService> _logger;

        public LoginManagerService(SecurePasswordDbContext context, ILoggerFactory loggerFactory, CryptographicSecurityService cryptographicSecurity)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _cryptographicSecurity = cryptographicSecurity;
            _logger = loggerFactory.CreateLogger<LoginManagerService>();
        }

        public Result CreateNewUser(string username, string password, string email)
        {
            NewUserCommand newUserCommand = new NewUserCommand(_loggerFactory, _context, _cryptographicSecurity);
            newUserCommand.AssignVariables(username, password, email);
            var result = newUserCommand.ExecuteWithValidation();

            return result;
        }

        public Result TryLogin(string? username, string? password)
        {
            TryLoginCommand tryLoginService = new TryLoginCommand(_context, _loggerFactory, _cryptographicSecurity);
            tryLoginService.AssignVariables(username, password);
            return tryLoginService.ExecuteWithValidation();
        }

    }
}
