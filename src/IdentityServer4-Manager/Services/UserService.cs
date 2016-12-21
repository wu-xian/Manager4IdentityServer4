﻿using AutoMapper;
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

        public async Task<PagingResponse> GetPaged(PagingRequest pagingRequest, string userId, string userName)
        {
            int totalCount = 0;
            var users = _userManager.Users
                    .Include(d => d.Claims)
                    .Include(d => d.Roles)
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
                    .ToList();
            return await Task.FromResult(new PagingResponse()
            {
                Total = totalCount,
                Rows = users
            });
        }

        public async Task<IList<Model.Claim>> GetClaims(string userId)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            if (usr == null)
                throw new ParamsWrongException(nameof(userId));
            var claims = await _userManager.GetClaimsAsync(usr);
            return Mapper.Map<IList<Model.Claim>>(claims);
        }

        public async Task<IList<string>> GetRoles(string userId)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(usr);
            return roles;
        }

        public async Task<IdentityResult> RemoveUserRole(string userId, string roleName)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            return await _userManager.RemoveFromRoleAsync(usr, roleName);

        }

        public async Task<IdentityResult> CreateUserRole(string userId, string roleName)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddToRoleAsync(usr, roleName);
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
            var userCurrentRoles = await _userManager.GetRolesAsync(usr);
            var removeRoles = userCurrentRoles.Except(roles).ToList();
            var addRoles = roles.Except(userCurrentRoles).ToList();
            var removeResult = await _userManager.RemoveFromRolesAsync(usr, removeRoles);
            var addResult = await _userManager.AddToRolesAsync(usr, addRoles);
            return removeResult.Succeeded || addResult.Succeeded;
        }

        public async Task<IdentityResult> CreateUser(string userName, string password)
        {
            var usr = new Model.IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                SecurityStamp = userName,
                UserName = userName
            };
            var result = await _userManager.CreateAsync(usr);
            _userManager.AddPasswordAsync(usr, password);
            return result;
        }

        public async Task<IdentityResult> CreateUserClaims(string userId, IEnumerable<Model.Claim> claims)
        {
            var userClaims = claims.Select(d => Mapper.Map<IdentityUserClaim<int>>(d).ToClaim()).ToList();
            var usr = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddClaimsAsync(usr, userClaims);
        }

        public async Task<IdentityResult> RemoveUserClaims(string userId, string claimType, string claimValue)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            var removeClaims = (await _userManager.GetClaimsAsync(usr)).Where(d => d.Value == claimValue && d.Type == claimType);
            return await _userManager.RemoveClaimsAsync(usr, removeClaims);
        }

        public async Task<IdentityResult> CreateUserClaims(string userId, string claimType, string claimValue)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            return await _userManager.AddClaimAsync(usr, new System.Security.Claims.Claim(claimType, claimValue));
        }

        public async Task<Model.IdentityUser> GetUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
