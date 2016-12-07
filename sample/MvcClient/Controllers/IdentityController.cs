using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    [Authorize]
    public class IdentityController : Controller
    {
        public IActionResult Get()
        {
            return Content("access");
        }
    }
}
