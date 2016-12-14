using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4_Manager.Model.ViewModel;
using IdentityServer4_Manager.Services;
using IdentityServer4.EntityFramework.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer4_Manager.Controllers
{
    public class IdentityResourceController : Controller
    {
        private readonly IdentityResourceService _identityResourceService;
        public IdentityResourceController(IdentityResourceService identityResourceService)
        {
            _identityResourceService = identityResourceService;
        }
        // GET: /<controller>/
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
        public async Task<IActionResult> Create(string name, string displayName, string description)
        {
            return Json(await _identityResourceService.Create(new IdentityResource()
            {
                Id = 0,
                Description = description,
                DisplayName = displayName,
                Name = name
            }));
        }
        public async Task<IActionResult> GetPaged(PagingRequest request)
        {
            return Json(await _identityResourceService.GetPaged(request));
        }

        public async Task<IActionResult> Detail(int id)
        {
            return Json(await _identityResourceService.Detail(id), new Newtonsoft.Json.JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        public async Task<IActionResult> Change(IdentityResource identityResource)
        {
            return Json(await _identityResourceService.Change(identityResource));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _identityResourceService.Delete(id));
        }
    }
}
