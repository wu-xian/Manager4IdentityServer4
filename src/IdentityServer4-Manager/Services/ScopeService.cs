using IdentityServer4.EntityFramework.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer4_Manager.Services
{
    public class ScopeService
    {
        public readonly ConfigurationDbContext _idb;
        public ScopeService(ConfigurationDbContext idb)
        {
            _idb = idb;
        }
        
    }
}
