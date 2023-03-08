using System.Diagnostics;
using BlazorGuiServer.Data.Repository;
using BlazorGuiServer.Data.Repository.Model;
using FluentResults;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
{
    public class TryLoginCommand : ValidatedCommand
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<TryLoginCommand> _logger;
        private readonly SecurePasswordDbContext _context;
        private readonly CryptographicSecurityService _cryptographicSecurity;

        private string? _username;
        private string? _password;

        public TryLoginCommand(SecurePasswordDbContext context, ILoggerFactory loggerFactory, CryptographicSecurityService cryptographicSecurity)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<TryLoginCommand>();
            _cryptographicSecurity = cryptographicSecurity;
        }
        public void AssignVariables(string? username, string? password)
        {
            this._logger.LogDebug("Calling AssignVariables");

            _username = username;
            _password = password;
        }
        public override Result Validate()
        {
            this._logger.LogDebug("Calling Validate");
            if (this._username == null || this._password == null)
            {
                this._logger.LogWarning("Username or password is null, missing validation in gui layer");
                return Result.Fail(new Error("Error, password or username is null"));
            }

            this.Validated = true;
            return Result.Ok();
        }
        public override Result Execute()
        {
            this._logger.LogDebug("Calling Execute");

            Debug.Assert(_username != null);
            Debug.Assert(_password != null);

            var userResult = new LoginHelper(_context, _loggerFactory).GetUser(_username);

            if (userResult.IsFailed)
            {
                return Result.Fail(userResult.Errors);
            }

            if (userResult.Value == null)
            {
                return Result.Fail(new Error("No user matches credentials"));
            }

            string hash = _cryptographicSecurity.CreateHashForPassword(_password, userResult.Value.Salt);

            if (hash != userResult.Value.Hash)
            {
                return Result.Fail(new Error("No user matches credentials"));
            }

            return Result.Ok();
        }
    }
}
