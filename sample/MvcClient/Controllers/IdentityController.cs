using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class IdentityController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return Content("im admin");
        }

        [Authorize(Roles = "user")]
        public IActionResult User1()
        {
            return Content("im user");

        }

        [Authorize(Roles = "admin,user")]
        public IActionResult Index()
        {
            return Content("im admin and user");
        }

        [Authorize(Roles = "saler")]
        public IActionResult Saler()
        {
            return Content("saler");
        }

        public IActionResult Ping()
        {
            return Content("pong");
        }
    }
}
