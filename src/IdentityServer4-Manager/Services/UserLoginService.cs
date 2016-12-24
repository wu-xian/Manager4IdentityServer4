using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Services
{
    public class UserLoginService
    {
        private readonly UserManager<Model.IdentityUser> _userManager;

        public UserLoginService(UserManager<Model.IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Check username and password against in-memory users
        /// 
        ///  摘要:
        /// Indicates password verification failed.
        /// Failed = 0,
        /// 
        ///  摘要:
        /// Indicates password verification was successful.
        /// Success = 1,
        /// 
        ///  摘要:
        /// Indicates password verification was successful however the password was encoded
        /// using a deprecated algorithm and should be rehashed and updated.
        /// SuccessRehashNeeded = 2
        /// 
        /// 
        /// </summary>
        public async Task<int> ValidateCredentials(string userName, string password)
        {
            var user = _userManager.Users.FirstOrDefault(d => d.UserName == userName);
            if (user != null)
            {
                return (int)new PasswordHasher<Model.IdentityUser>().VerifyHashedPassword(user, user.PasswordHash, password);
            }

            return 0;
        }

        public async Task<Model.IdentityUser> FindByUsername(string userName)
        {
            return await Task.FromResult(_userManager.Users.FirstOrDefault(d => d.UserName == userName));
        }
    }
}
