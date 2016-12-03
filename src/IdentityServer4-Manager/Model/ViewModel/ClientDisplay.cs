using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class ClientDisplay
    {
        public int Id { set; get; }
        public string ClientId { set; get; }
        public string ClientName { set; get; }
        public string ClientURI { set; get; }

        public List<ClientScope> Scopes { set; get; }
    }
}
