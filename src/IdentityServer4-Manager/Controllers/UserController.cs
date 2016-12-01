using IdentityServer4_Manager.Model.ViewModel;
using IdentityServer4_Manager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ClaimView(string userId)
        {
            var claims = await _userService.GetClaims(userId);
            return PartialView("UserClaims", claims);
        }

        public async Task<IActionResult> RoleView(string userId)
        {
            var roles = await _userService.GetRoles(userId);
            return PartialView("UserRoles", roles);
        }

        public async Task<IActionResult> RemoveClaim(string userId, string claimType, string claimValue)
        {
            return Json(await _userService.RemoveUserClaims(userId, claimType, claimValue));
        }

        public async Task<IActionResult> CreateClaim(string userId, string claimType, string claimValue)
        {
            return Json(await _userService.CreateClaims(userId, claimType, claimValue));
        }

        public async Task<IActionResult> GetUsers(PagingRequest request, string userId, string userName)
        {
            System.Threading.Thread.Sleep(2000);
            var result = await _userService.GetDisplayUsers(request, userId, userName);
            return Json(result);
        }

        public async Task<IActionResult> AddUser()
        {
            string userName = Guid.NewGuid().ToString();
            return Json(await _userService.Add(new Model.IdentityUser()
            {
                UserName = userName,
                Email = "wu-xian.cool@qq.com"
            }));
        }
    }
}
