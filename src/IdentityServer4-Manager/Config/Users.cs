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
                Email = "wu-xian.cool@qq.com"
            };
            idUser1.PasswordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(idUser1, "wuxian");

            var IdUsr2 = new Model.IdentityUser()
            {
                UserName = "admin"
                ,
            };
            new PasswordHasher<Model.IdentityUser>().HashPassword(IdUsr2, "helloworld");
            return new List<Model.IdentityUser>()
            {
                idUser1,
                IdUsr2
            };
        }
    }
}
