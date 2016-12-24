﻿using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Manager.Extension;
using IdentityServer4.Manager.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Services
{
    public class ApiResourceService
    {
        private readonly ConfigurationDbContext _idb;
        public ApiResourceService(ConfigurationDbContext idb)
        {
            _idb = idb;
        }

        public async Task<PagingResponse> GetPaged(PagingRequest request)
        {
            int totalCount = 0;
            var dbResult = _idb.ApiResources
                .Include(d => d.Scopes)
                .Include(d => d.Secrets)
                .Include(d => d.UserClaims)
                .Paged(
                d => true,
                request.Order,
                request.Offset,
                request.Limit,
                request.isAsc,
                ref totalCount
                )
                .Select(d => Mapper.Map<ApiResourceDisplay>(d))
                .ToList();
            return await Task.FromResult(new PagingResponse()
            {
                Rows = dbResult,
                Total = totalCount
            });
        }

        public async Task<int> Create(ApiResource apiResource)
        {
            _idb.ApiResources.Add(apiResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Update(ApiResource apiResource)
        {
            _idb.ApiResources.Update(apiResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            _idb.ApiResources.Remove(new ApiResource()
            {
                Id = id
            });
            return await _idb.SaveChangesAsync();
        }

        public async Task<ApiResource> Detail(int id)
        {
            return await _idb.ApiResources
                .Include(d => d.UserClaims)
                .Include(d => d.Scopes)
                .Include(d => d.Secrets)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Change(ApiResource apiResource)
        {
            _idb.ApiResources.Remove(new ApiResource()
            {
                Id = apiResource.Id
            });
            await _idb.SaveChangesAsync();
            apiResource.Id = 0;
            _idb.ApiResources.Add(apiResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> UpdateScopes(int id, List<string> scopes)
        {
            var apiResource = await _idb.ApiResources.FirstOrDefaultAsync(d => d.Id == id);
            if (apiResource == null)
            {
                return 0;
            }
            var apiScopes = new List<ApiScope>();
            foreach (var item in scopes)
            {
                apiScopes.Add(new ApiScope()
                {
                    Name = item,
                    ApiResource = apiResource,
                    DisplayName = item
                });
            }
            apiResource.Scopes = apiScopes;
            _idb.ApiResources.Update(apiResource);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> DeleteScopes(int id, string scope)
        {
            return 0;
        }
    }
}
