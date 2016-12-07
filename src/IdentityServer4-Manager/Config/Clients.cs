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
                   ClientId="mvc-client-one",
                   //ClientUri="http://localhost:9091",
                   AllowedScopes= {
                        "MVC.ADMIN",
                        "MVC.USER",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                    },
                   AllowedGrantTypes= GrantTypes.Implicit,
                   //RequireClientSecret=false,
                   RedirectUris= { "http://localhost:9091/signin-oidc"},
                   LogoutUri=  "http://localhost:9091"
                }
            };
        }
    }
}
