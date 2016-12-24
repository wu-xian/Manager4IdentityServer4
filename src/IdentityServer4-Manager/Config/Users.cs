using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Config
{
    public class Users
    {
        public static List<Model.IdentityUser> Get()
        {
            var idUser1 = new Model.IdentityUser()
            {
                Id = "wuxian",
                UserName = "wuxian",
                Email = "wu-xian.cool@qq.com",
                Roles = { new IdentityUserRole<string>() {
                    RoleId="MVC.ADMIN"
                } },
                SecurityStamp = "wuxian"
            };
            idUser1.PasswordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(idUser1, "wuxian");
            var idUser2 = new Model.IdentityUser()
            {
                Id = "admin",
                UserName = "admin",
                Email = "admin.cool@qq.com"
            };
            idUser2.PasswordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(idUser2, "admin");
            return new List<Model.IdentityUser>()
            {
                idUser1,
                idUser2
            };
        }

        public static List<System.Security.Claims.Claim> GetClaims()
        {
            return new List<System.Security.Claims.Claim>() {
                new System.Security.Claims.Claim("role","user"),
                new System.Security.Claims.Claim("name","default"+Guid.NewGuid().ToString())
            };
        }
    }
}
