using AutoMapper;
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
    public class ClientService
    {
        private readonly ConfigurationDbContext _idb;
        public ClientService(ConfigurationDbContext idb)
        {
            _idb = idb;
        }

        public PagingResponse GetPaged(PagingRequest request, string clientId, string clientName)
        {
            int totalCount = 0;
            var dbResult = _idb.Clients
                .Include(d => d.AllowedScopes)
                .Paged(d =>
                        ((string.IsNullOrEmpty(clientId) || (d.ClientId == clientId)) &&
                        ((string.IsNullOrEmpty(clientName) || (d.ClientName == clientName)))
                    ),
                    request.Order,
                    request.Offset,
                    request.Limit,
                    request.isAsc,
                    ref totalCount
                    )
                .Select(d => Mapper.Map<Model.ViewModel.ClientDisplay>(d))
                ;
            return new PagingResponse()
            {
                Total = totalCount,
                Rows = dbResult
            };
        }

        public void AddClient(string clientName, string clientUri)
        {
            var clien = new Client()
            {
                Id = 0,
                ClientId = Guid.NewGuid().ToString(),
                ClientName = clientName,
                ClientUri = clientUri

            };
            _idb.Clients.Add(clien);
            _idb.SaveChanges();
        }

        public async Task RemoveClient(int id)
        {
            _idb.Clients.Remove(new Client()
            {
                Id = id
            });
            await _idb.SaveChangesAsync();
        }

        public Client Get(int id)
        {
            var dbResult = _idb.Clients.Where(d => d.Id == id).AsNoTracking().FirstOrDefault();
            return dbResult;
        }

        public async Task<List<ClientScope>> GetScopes(int id)
        {
            _idb.
        }
    }
}
