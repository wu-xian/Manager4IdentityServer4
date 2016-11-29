using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class RoleController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
