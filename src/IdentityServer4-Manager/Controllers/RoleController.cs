using IdentityServer4_Manager.Model.ViewModel;
using IdentityServer4_Manager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPaged(PagingRequest request, RoleCondition condition)
        {
            return Json(await _roleService.GetPaged(request));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            return Json(_roleService.Create(roleName));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string roleId)
        {
            var role = await _roleService.GetById(roleId);
            return PartialView("Update", role);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string roleId, string roleName)
        {
            return Json(await _roleService.Update(roleId, roleName));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string roleId)
        {
            return Json(await _roleService.Delete(roleId));
        }

        #region Claim
        [HttpGet]
        public async Task<IActionResult> Claims(string roleId)
        {
            ViewBag.RoleId = roleId;
            var claims = await _roleService.GetClaims(roleId);
            return PartialView("Claims", claims);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClaim(string roleId, string claimType, string claimValue)
        {
            return Json(await _roleService.CreateClaims(roleId, claimType, claimValue));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClaim(string roleId, string claimType)
        {
            return Json(await _roleService.DeleteClaims(roleId, claimType));
        }
        #endregion

        #region User
        [HttpGet]
        public async Task<IActionResult> Users(string roleName)
        {
            var users = await _roleService.GetUsers(roleName);
            ViewBag.RoleName = roleName;
            return PartialView("Users", users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(string roleName, string userId)
        {
            return Json(await _roleService.DeleteUserFromRole(roleName, userId));
        }
        #endregion
    }
}
