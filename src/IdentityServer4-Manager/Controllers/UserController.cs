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

        public async Task<IActionResult> GetUsers()
        {
            System.Threading.Thread.Sleep(2000);
            return Json(new PagingResponse()
            {
                Total = 50,
                Rows = _userService.Get()
            });
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
