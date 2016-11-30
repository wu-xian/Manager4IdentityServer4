using AutoMapper;
using IdentityServer4_Manager.Exceptions;
using IdentityServer4_Manager.Extension;
using IdentityServer4_Manager.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Services
{
    public class UserService
    {
        private readonly UserManager<Model.IdentityUser> _userManager;
        private readonly RoleManager<Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole> _roleManager;

        public UserService(UserManager<Model.IdentityUser> userManager,
            RoleManager<Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole> roleManager)
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

        public async Task<PagingResponse> GetDisplayUser(PagingRequest pagingRequest, string userId, string userName)
        {
            int totalCount = 0;
            var users = _userManager.Users
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
            return new PagingResponse()
            {
                Total = totalCount,
                Rows = users
            };
        }

        public async Task<IdentityResult> EditUserRole(string userId, List<string> roles)
        {
            var usr = await _userManager.FindByIdAsync(userId);
            if (usr == null)
            {
                throw new ParamsWrongException(nameof(usr));
            }
            return null;
        }
    }
}
