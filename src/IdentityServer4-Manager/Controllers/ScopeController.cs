﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Controllers
{
    public class ScopeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
