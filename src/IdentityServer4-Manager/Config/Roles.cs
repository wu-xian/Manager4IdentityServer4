using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Config
{
    public class Roles
    {
        public static List<IdentityRole> Get()
        {
            return new List<IdentityRole>() {
                new IdentityRole() {
                   Name="MVC.ADMIN"
                },
                new IdentityRole() {
                    Name="MVC.USER"
                }
            };
        }
    }
}
