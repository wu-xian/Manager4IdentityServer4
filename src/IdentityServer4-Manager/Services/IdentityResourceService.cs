using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Manager.Extension;
using IdentityServer4.Manager.Model;
using IdentityServer4.Manager.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Services
{
    public class IdentityResourceService
    {
        private readonly ConfigurationDbContext _idb;
        public IdentityResourceService(ConfigurationDbContext idb)
        {
            _idb = idb;
        }

        public async Task<PagingResponse> GetPaged(PagingRequest request)
        {
            int totalCount = 0;
            var dbResult = await _idb.IdentityResources
                .Include(d => d.UserClaims)
                .Paged(
                d => true,
                request.Order,
                request.Offset,
                request.Limit,
                request.isAsc,
                ref totalCount)
                .Select(d => Mapper.Map<Model.ViewModel.IdentityResourceDisplay>(d))
                .ToListAsync();
            return await Task.FromResult<PagingResponse>(new PagingResponse()
            {
                Rows = dbResult,
                Total = totalCount
            });
        }

        public async Task<IdentityResource> GetById(int id)
        {
            return await _idb.IdentityResources.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Create(IdentityResource identityResource)
        {
            _idb.IdentityResources.Add(identityResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Update(IdentityResource identityResource)
        {
            _idb.Update(identityResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            _idb.IdentityResources.Remove(new IdentityResource()
            {
                Id = id
            });
            return await _idb.SaveChangesAsync();
        }

        public async Task<IdentityResource> Detail(int id)
        {
            return await _idb.IdentityResources
                .Include(d => d.UserClaims)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Change(IdentityResource identityResource)
        {
            _idb.IdentityResources.Remove(new IdentityResource()
            {
                Id = identityResource.Id
            });
            await _idb.SaveChangesAsync();
            identityResource.Id = 0;
            _idb.IdentityResources.Add(identityResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<List<Claim>> GetClaimsById(int id)
        {
            var identityResource = await _idb.IdentityResources.FirstOrDefaultAsync(d => d.Id == id);
            if (identityResource == null)
                return null;
            return identityResource.UserClaims.Select(d => Mapper.Map<Model.Claim>(d)).ToList();
        }

        public async Task<int> UpdateClaims(int id, List<string> claims)
        {
            var identityResource = await _idb.IdentityResources.FirstOrDefaultAsync(d => d.Id == id);
            if (identityResource == null)
                return 0;
            var identityClaims = new List<IdentityClaim>();
            foreach (var item in claims)
            {
                identityClaims.Add(new IdentityClaim()
                {
                    IdentityResource = identityResource,
                    Type = item
                });
            }
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> DeleteClaims(int id, int claimId)
        {
            var identityResource = await _idb.IdentityResources.FirstOrDefaultAsync(d => d.Id == id);
            if (identityResource == null)
            {
                return 0;
            }
            identityResource.UserClaims.RemoveAll(d => d.Id == claimId);
            return await _idb.SaveChangesAsync();
        }
    }
}
