using IdentityServer4.Manager.Model.ViewModel;
using IdentityServer4.Manager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string userName, string password)
        {
            return Json(await _userService.CreateUser(userName, password));
        }

        [HttpGet]
        public async Task<IActionResult> Claims(string userId)
        {
            var claims = (await _userService.GetClaims(userId)).ToList();
            ViewBag.UserId = userId;
            return PartialView("Claims", claims);
        }

        [HttpGet]
        public async Task<IActionResult> Roles(string userId)
        {
            var roles = await _userService.GetRoles(userId);
            ViewBag.UserId = userId;
            return PartialView("Roles", roles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClaim(string userId, string claimType, string claimValue)
        {
            return Json(await _userService.RemoveUserClaims(userId, claimType, claimValue));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClaim(string userId, string claimType, string claimValue)
        {
            return Json(await _userService.CreateUserClaims(userId, claimType, claimValue));
        }

        public async Task<IActionResult> GetPaged(PagingRequest request, string userId, string userName)
        {
            var result = await _userService.GetPaged(request, userId, userName);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            try
            {
                var identityResult = await _userService.CreateUserRole(userId, roleName);
                return Json(identityResult);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
        {
            return Json(await _userService.RemoveUserRole(userId, roleName));
        }
    }
}
