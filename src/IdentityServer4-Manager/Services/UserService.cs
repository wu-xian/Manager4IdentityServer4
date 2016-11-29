using AutoMapper;
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

        public UserService(UserManager<Model.IdentityUser> userManager)
        {
            _userManager = userManager;
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
    }
}
