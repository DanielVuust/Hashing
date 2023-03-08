using System.Diagnostics;
using BlazorGuiServer.Data.Repository;
using BlazorGuiServer.Data.Repository.Model;
using FluentResults;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Services.ServiceHelpers
{
    public class NewUserCommand : ValidatedCommand
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<NewUserCommand> _logger;
        private readonly SecurePasswordDbContext _context;
        private readonly CryptographicSecurityService _cryptographicSecurity;

        private string? _username;
        private string? _password;
        private string? _email;
        public NewUserCommand(ILoggerFactory loggerFactory, SecurePasswordDbContext context, CryptographicSecurityService cryptographicSecurity)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<NewUserCommand>();
            _context = context;
            _cryptographicSecurity = cryptographicSecurity;
        }

        public void AssignVariables(string username, string password, string email)
        {
            _logger.LogDebug("Calling AssignVariables");

            _username = username;
            _password = password;
            _email = email;
        }

        public override Result Execute()
        {
            Debug.Assert(_username != null);
            Debug.Assert(_password != null);
            Debug.Assert(_email != null);

            _logger.LogDebug("Calling Execute");

            string salt = _cryptographicSecurity.CreateSalt();
            string hash = _cryptographicSecurity.CreateHashForPassword(_password, salt);

            NewUserHelper helper = new NewUserHelper(_context, _loggerFactory);
            Result<User> result = helper.CreateNewUser(_username, hash, _email, salt);

            if (result.IsFailed)
                return Result.Fail(new Error("An error occurred when trying to save new user to db"));
            return Result.Ok();
        }

        public override Result Validate()
        {
            _logger.LogDebug("Calling Validate");

            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                //passwordText is used for debugging purposes and is hidden if not empty.
                var passwordText = string.IsNullOrEmpty(_password) ? "null" : "XXXXXXX";
                _logger.LogWarning($"Username: {_username}, password: {passwordText} and/or email: {_email} is null");
                return Result.Fail(new Error("Username, password or email is null"));
            }

            if (_context.Users.Any(x => x.Username == _username))
            {
                _logger.LogDebug($"Username {_username} already in use");
                return Result.Fail(new Error("Username is already in use"));
            }
            if (_context.Users.Any(x => x.Email == _email))
            {
                _logger.LogDebug($"Email {_email} already in use");
                return Result.Fail(new Error("Email is already in use"));
            }

            Validated = true;
            return Result.Ok();
        }
    }
}
