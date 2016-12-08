using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

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
                   Claims= new List<System.Security.Claims.Claim>() {
                       new System.Security.Claims.Claim("client.role","clientROLE"),
                       new System.Security.Claims.Claim("client.name","clientNAME")
                   },
                   AllowAccessTokensViaBrowser=true,
                   PrefixClientClaims=true,
                   AlwaysSendClientClaims=true,
                   AllowedGrantTypes= GrantTypes.Implicit,
                   //RequireClientSecret=false,
                   RedirectUris= { "http://localhost:9091/signin-oidc"},
                   PostLogoutRedirectUris= {"http://localhost:9091" }
                }
            };
        }
    }
}
