using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4_Manager.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServer4_Manager.Controllers
{
    public class HomeController : Controller
    {
        //private HttpContext _httpContext;
        //public HomeController(HttpContextAccessor httpContext)
        //{
        //    _httpContext = httpContext?.HttpContext;
        //}
        public HomeController() { }

        public IActionResult Index()
        {
            var usr = HttpContext?.User;
            if (usr == null) return Content("your are not login");
            return View((from u in usr.Claims select new Claim() { ClaimType = u.ValueType, ClaimValue = u.Value }).ToList());
        }

        [Authorize]
        public IActionResult Identity()
        {
            return Content("Identity Content");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
