using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Model.IdentityUser> _signInManager;
        private readonly UserManager<Model.IdentityUser> _userManager;

        public AccountController(SignInManager<Model.IdentityUser> signInManager, UserManager<Model.IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Login()
        {
            var usr = new Model.IdentityUser()
            {
                UserName = "wu-xian",
                Email = "wu-xian.cool@qq.com",
                PhoneNumber = "15101571730"
            };
            usr.PasswordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(usr, "wuxian123");
            await _userManager.CreateAsync(usr);
            await _userManager.AddClaimAsync(await _userManager.FindByNameAsync("wu-xian"), new Claim(JwtClaimTypes.Role, "im role"));


            return Ok();
        }
    }
}
