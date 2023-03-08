using BlazorGuiServer.Data;
using Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorGuiServer.Data.Management.Services.ServiceHelpers;
using FluentResults;
using HashingDomain.Model;
using BlazorGuiServer.Data.Repository;
using BlazorGuiServer.Data.Repository.Model;

namespace BlazorGuiServer.Data.Management.Services
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

        /// <summary>
        ///     Tries to login for a user with username and password
        /// </summary>
        /// <returns>
        ///     True for successful login, false for everything else
        /// </returns>
        public Result TryLogin(string? username, string? password)
        {
            TryLoginCommand tryLoginService = new TryLoginCommand(this._context, _loggerFactory, _cryptographicSecurity);
            tryLoginService.AssignVariables(username, password);
            Result result = tryLoginService.ExecuteWithValidation();

            if (result.IsFailed)
            {
                return result;
            }
            return Result.Ok();
        }
        
    }
}
