using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Manager.Services;
using IdentityServer4.Manager.Model.ViewModel;
using IdentityServer4.EntityFramework.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer4.Manager.Controllers
{
    public class ApiResourceController : Controller
    {
        private readonly ApiResourceService _apiResourceService;
        public ApiResourceController(ApiResourceService identityResourceService)
        {
            _apiResourceService = identityResourceService;
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
            return Json(await _apiResourceService.Create(new ApiResource()
            {
                Id = 0,
                Description = description,
                DisplayName = displayName,
                Name = name
            }));
        }

        public async Task<IActionResult> GetPaged(PagingRequest request)
        {
            return Json(await _apiResourceService.GetPaged(request));
        }

        public async Task<IActionResult> Detail(int id)
        {
            return Json(await _apiResourceService.Detail(id), new Newtonsoft.Json.JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        public async Task<IActionResult> Change(ApiResource identityResource)
        {
            return Json(await _apiResourceService.Change(identityResource));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _apiResourceService.Delete(id));
        }
    }
}
