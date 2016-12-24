
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Config
{
    public class IdentityResource
    {
        public static List<IdentityServer4.Models.IdentityResource> Get()
        {
            return new List<IdentityServer4.Models.IdentityResource>() {
                new IdentityServer4.Models.IdentityResource() {
                    Name="MVC.ADMIN",
                    Enabled=true,
                    DisplayName="MVC.ADMIN",
                    UserClaims= { "role","name" },
                    Emphasize=true
                },
                new IdentityServer4.Models.IdentityResource() {
                    Name="MVC.USER",
                    Enabled=true,
                    DisplayName="MVC.USER"
                },
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile()
            };
        }
    }
}
