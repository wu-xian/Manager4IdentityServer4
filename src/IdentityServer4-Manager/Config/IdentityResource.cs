using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Config
{
    public class IdentityResource
    {
        public static List<IdentityServer4.Models.IdentityResource> Get()
        {
            return new List<IdentityServer4.Models.IdentityResource>() {
                new IdentityServer4.Models.IdentityResource() {
                    DisplayName="MVC.ADMIN",
                    Name="MVC.ADMIN",
                    ShowInDiscoveryDocument=true,
                    Enabled=true
                },
                new IdentityServer4.Models.IdentityResource() {
                    DisplayName="MVC.USER",
                    Name="MVC.USER",
                    ShowInDiscoveryDocument=true,
                    Enabled=true
                }
            };
        }
    }
}
