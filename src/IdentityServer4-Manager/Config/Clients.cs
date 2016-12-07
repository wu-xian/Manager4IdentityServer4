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
                   ClientName="MVC",
                   ClientId=Guid.NewGuid().ToString(),
                   ClientUri="http://localhost:9091",
                   AllowedScopes= {
                        "MVC.ADMIN",
                        "MVC.USER",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                    },
                   RedirectUris= { "http://localhost:9091/home/index#wuxian"},
                   LogoutUri=  "http://localhost:9091/account/logout"
                }
            };
        }
    }
}
