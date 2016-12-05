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
            var idUser = new Model.IdentityUser()
            {
                UserName = "wuxian",
                Email = "wu-xian.cool@qq.com"
            };
            idUser.PasswordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(idUser, "wuxian");
            return new List<Model.IdentityUser>()
            {
                idUser
            };
        }
    }
}
