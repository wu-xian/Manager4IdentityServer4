using AutoMapper;
using IdentityServer4_Manager.Exceptions;
using IdentityServer4_Manager.Extension;
using IdentityServer4_Manager.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Services
{
    public class UserService
    {
        private readonly UserManager<Model.IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<Model.IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserDisplay> Get()
        {
            return _userManager.Users.Select(u =>
                Mapper.Map<UserDisplay>(u)).ToList();
        }

        public async Task<IdentityResult> Add(Model.IdentityUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<PagingResponse> GetDisplayUsers(PagingRequest pagingRequest, string userId, string userName)
        {
            int totalCount = 0;
            var users = await _userManager.Users
                    .Paged(
                    d =>
                        (string.IsNullOrEmpty(userId) || d.Id == userId) &&
                        (string.IsNullOrEmpty(userName) || d.UserName.Contains(userName)),
                        pagingRequest.Order,
                        pagingRequest.Offset,
                        pagingRequest.Limit,
                        pagingRequest.isAsc,
                        ref totalCount
                        )
                    .Select(d => Mapper.Map<UserDisplay>(d))
                    .ToListAsync();
            return new PagingResponse()
            {
                Total = totalCount,
                Rows = users
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">the id for specified user</param>
        /// <param name="roles">roles name after change</param>
        /// <returns></returns>
        public async Task<bool> EditUserRole(string userId, List<string> roles)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            if (usr == null)
            {
                throw new ParamsWrongException(nameof(userId));
            }
            var userCurrentRoles = await _userManager.GetRolesAsync(usr);
            var removeRoles = userCurrentRoles.Except(roles).ToList();
            var addRoles = roles.Except(userCurrentRoles).ToList();
            var removeResult = await _userManager.RemoveFromRolesAsync(usr, removeRoles);
            var addResult = await _userManager.AddToRolesAsync(usr, addRoles);
            return removeResult.Succeeded || addResult.Succeeded;
        }

        public async Task<IdentityResult> CreateUser(string userName, string password, string email, string phoneNumber)
        {
            var usr = new Model.IdentityUser()
            {
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            var result = await _userManager.CreateAsync(usr);
            _userManager.AddPasswordAsync(usr, password);
            return result;
        }

        public async Task<IdentityResult> CreateUserClaims(string userId, IEnumerable<Model.Claim> claims)
        {
            var userClaims = claims.Select(d => Mapper.Map<IdentityUserClaim<int>>(d).ToClaim()).ToList();
            var usr = await _userManager.FindByIdAsync(userId);
            if (usr == null)
                throw new ParamsWrongException(nameof(userId));
            return await _userManager.AddClaimsAsync(usr, userClaims);
        }

        public async Task<IdentityResult> RemoveUserClaims(string userId, Dictionary<string,string> claims)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            if (usr == null)
                throw new ParamsWrongException(nameof(userId));
            var removeClaims = (await _userManager.GetClaimsAsync(usr))
                .Where(d=>d.ValueType==;
            await _userManager.RemoveClaimsAsync(usr,)
        }
    }
}
