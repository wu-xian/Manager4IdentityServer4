using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class InformationController : Controller
    {
        private ConfigurationDbContext _idb;
        public InformationController(ConfigurationDbContext idb)
        {
            _idb = idb;
        }
        public IActionResult Index()
        {
            return Content("no info");
        }
    }
}
