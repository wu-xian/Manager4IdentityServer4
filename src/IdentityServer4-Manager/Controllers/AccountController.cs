using IdentityModel;
using IdentityServer4_Manager.Model.ViewModel;
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

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var usr = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (usr == null)
            {
                return Content("login fail");
            }
            var passwordHash = new PasswordHasher<Model.IdentityUser>().HashPassword(usr, loginViewModel.Password);
            if (string.Equals(passwordHash, usr.PasswordHash))
            {
                await _signInManager.SignInAsync(usr, true);
                return Content("success");
            }
            return Content("wrong password");
        }
    }
}
