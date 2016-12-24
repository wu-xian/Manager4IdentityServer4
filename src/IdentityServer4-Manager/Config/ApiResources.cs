using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Config
{
    public class ApiResources
    {
        public static List<ApiResource> Get()
        {
            return new List<ApiResource>() {
                new ApiResource() {
                    DisplayName="MVC.ADMIN",
                    Name="mvc-ALL",
                    Scopes=new List<Scope>() {
                        new Scope("mvc-admin")
                    }
                }
            };
        }
    }
}
