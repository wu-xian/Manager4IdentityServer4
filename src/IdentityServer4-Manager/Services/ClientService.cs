﻿using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4_Manager.Extension;
using IdentityServer4_Manager.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Services
{
    public class ClientService
    {
        private readonly ConfigurationDbContext _idb;
        public ClientService(ConfigurationDbContext idb)
        {
            _idb = idb;
        }

        public async Task<PagingResponse> GetPaged(PagingRequest request, string clientId, string clientName)
        {
            int totalCount = 0;
            //var dbResult = _idb.Clients
            //    .Include(d => d.AllowedScopes)
            //    .Paged(d =>
            //            ((string.IsNullOrEmpty(clientId) || (d.ClientId == clientId)) &&
            //            ((string.IsNullOrEmpty(clientName) || (d.ClientName == clientName)))
            //        ),
            //        request.Order,
            //        request.Offset,
            //        request.Limit,
            //        request.isAsc,
            //        ref totalCount
            //        )
            //    .Select(d => Mapper.Map<Model.ViewModel.ClientDisplay>(d))
            //    .ToList()
            //;
            var dbResult = _idb.Clients
                .Include(d => d.AllowedScopes)
                .Where(d =>
                        ((string.IsNullOrEmpty(clientId) || (d.ClientId == clientId)) &&
                        ((string.IsNullOrEmpty(clientName) || (d.ClientName == clientName))))
                        )
                .ToPagedList(request.Limit, 1)
                .Select(d => Mapper.Map<Model.ViewModel.ClientDisplay>(d));
            return await Task.FromResult<PagingResponse>(new PagingResponse()
            {
                Total = totalCount,
                Rows = dbResult
            });
        }

        public async Task<int> Create(string clientName, string clientUri)
        {
            var clien = new Client()
            {
                Id = 0,
                ClientId = Guid.NewGuid().ToString(),
                ClientName = clientName,
                ClientUri = clientUri

            };
            _idb.Clients.Add(clien);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            _idb.Clients.Remove(new Client()
            {
                Id = id
            });
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> Update(Client client)
        {
            _idb.Clients.Update(client);
            return await _idb.SaveChangesAsync();
        }

        public async Task<Client> Detail(int id)
        {
            return await _idb.Clients
                .Include(d => d.AllowedScopes)
                .Include(d => d.ClientSecrets)
                .Include(d => d.RedirectUris)
                .Include(d => d.PostLogoutRedirectUris)
                .Include(d => d.Claims)
                .Include(d => d.AllowedCorsOrigins)
                .Include(d => d.AllowedGrantTypes)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> Change(Client client)
        {
            var db_client = _idb.Clients.Remove(new Client()
            {
                Id = client.Id
            });
            await _idb.SaveChangesAsync();
            client.Id = 0;
            _idb.Clients.Add(client);
            return await _idb.SaveChangesAsync();
        }

        public async Task<Client> GetById(int id)
        {
            return await _idb.Clients.Where(d => d.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<ClientScope>> GetScopesByClientId(int id)
        {
            var client = await _idb.Clients.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (client == null)
            {
                return null;
            }
            return await Task.FromResult<List<ClientScope>>(client.AllowedScopes);
        }

        public async Task<int> UpdateScope(int id, List<string> scope)
        {
            var client = await _idb.Clients
                .Include(d => d.AllowedScopes)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (client == null || scope == null)
            {
                return 0;
            }
            var clientScopes = new List<ClientScope>();
            foreach (var item in scope)
            {
                clientScopes.Add(new ClientScope()
                {
                    Client = client,
                    Scope = item
                });
            }
            client.AllowedScopes = clientScopes;
            _idb.Clients.Update(client);
            return await _idb.SaveChangesAsync();
        }

        public async Task<int> DeleteScope(int id, string scope)
        {
            var client = await _idb.Clients.Include(d => d.AllowedScopes).FirstOrDefaultAsync(d => d.Id == id);
            if (client == null)
            {
                return 0;
            }
            client.AllowedScopes.RemoveAll(d => d.Scope == scope);
            return await _idb.SaveChangesAsync();
        }
    }
}
