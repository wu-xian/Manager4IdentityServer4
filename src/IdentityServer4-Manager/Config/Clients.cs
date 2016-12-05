using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>() {
                new Client() {
                   ClientName="Mvc-Client",
                   ClientId=Guid.NewGuid().ToString(),
                   ClientUri="http://localhost:9091"
                }
            };
        }
    }
}
