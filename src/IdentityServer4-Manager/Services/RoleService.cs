using AutoMapper;
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
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<PagingResponse> GetPaged(PagingRequest pagingRequest)
        {
            int totalCount = 0;
            var dbResult = await _roleManager.Roles
                .Paged(
                    d => true,
                    pagingRequest.Order,
                    pagingRequest.Offset,
                    pagingRequest.Limit,
                    pagingRequest.isAsc,
                    ref totalCount
                    )
                 .ToListAsync();
            return new PagingResponse()
            {
                Rows = dbResult,
                Total = totalCount
            };
        }

        public async Task<IdentityResult> Create(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = string.Empty,
                Name = roleName,
                NormalizedName = roleName
            });
        }

        public async Task<IdentityResult> Update(string id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            role.Name = roleName;
            role.NormalizedName = roleName;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> Delete(string id)
        {
            return await _roleManager.DeleteAsync(new IdentityRole()
            {
                Id = id
            });
        }

        public async Task<List<Model.Claim>> GetClaims(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return null;
            return (await _roleManager.GetClaimsAsync(role))
                .Select(d => Mapper.Map<Model.Claim>(d))
                .ToList();
        }

        public async Task<IdentityResult> CreateClaims(string id, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return null;
            return await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claimType, claimValue));
        }

        public async Task<IdentityResult> DeleteClaims(string id, string claimType)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return null;
            var claim = (await _roleManager.GetClaimsAsync(role)).FirstOrDefault(d => d.Type == claimType);
            return await _roleManager.RemoveClaimAsync(role, claim);
        }
    }
}
