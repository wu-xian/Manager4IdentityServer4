using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Config
{
    public class Users
    {
        public static List<Model.IdentityUser> Get()
        {
            var idUser1 = new Model.IdentityUser()
            {
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
    }
}
