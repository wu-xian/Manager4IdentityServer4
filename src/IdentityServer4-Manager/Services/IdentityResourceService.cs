using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4_Manager.Extension;
using IdentityServer4_Manager.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Services
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
            var dbResult = await _idb.IdentityResources.Paged(
                d => true,
                request.Order,
                request.Offset,
                request.Limit,
                request.isAsc,
                ref totalCount)
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
            return await Task.FromResult<int>(1);
        }
    }
}
