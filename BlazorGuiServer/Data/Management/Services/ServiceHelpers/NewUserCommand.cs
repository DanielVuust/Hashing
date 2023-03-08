using System.Diagnostics;
using BlazorGuiServer.Data.Management.Services;
using BlazorGuiServer.Data.Repository;
using BlazorGuiServer.Data.Repository.Model;
using FluentResults;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
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
            this._logger.LogDebug("Calling AssignVariables");

            _username = username;
            _password = password;
            _email = email;

            Validated = true;
        }

        public override Result Execute()
        {
            Debug.Assert(_username != null);
            Debug.Assert(_password != null);
            Debug.Assert(_email != null);

            this._logger.LogDebug("Calling Execute");

            string salt = _cryptographicSecurity.CreateSalt();
            string hash = _cryptographicSecurity.CreateHashForPassword(_password, salt);

            NewUserHelper helper = new NewUserHelper(_context, _loggerFactory);
            Result<User> result;
            try
            {
                result = helper.CreateNewUser(_username, hash, _email, salt);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Could not create new user. ex: {ex}");
                return Result.Fail(new Error("Could not create new user").CausedBy(ex));
            }

            if (result.IsFailed)
                return Result.Fail(new Error("An error occurred when trying to save new user to db"));
            return Result.Ok();
        }

        public override Result Validate()
        {
            this._logger.LogDebug("Calling Validate");

            //Validate input.
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_email))
            {
                //passwordText is used for debugging purposes and is hidden if not empty.
                var passwordText = string.IsNullOrEmpty(_password) ? "null" : "XXXXXXX";
                _logger.LogWarning($"Username: {_username}, password: {passwordText} and/or email: {_email} is null");
                return Result.Fail(new Error("Username, password or email is null"));
            }

            Validated = true;
            return Result.Ok();
        }
    }
}
