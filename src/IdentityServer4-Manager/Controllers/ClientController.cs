using IdentityServer4.EntityFramework.Entities;
using IdentityServer4_Manager.Model.ViewModel;
using IdentityServer4_Manager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string clientName, string clientUri)
        {
            return Json(await _clientService.Create(clientName, clientUri));
        }

        public async Task<IActionResult> GetPaged(PagingRequest request, string clientId, string clientName)
        {
            return Json(await _clientService.GetPaged(request, clientId, clientName));
        }

        public async Task<IActionResult> Detail(int id)
        {
            return Json(await _clientService.Detail(id), new Newtonsoft.Json.JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        public async Task<IActionResult> Change([FromForm] Client client)
        {
            return Json(await _clientService.Change(client));
        }

        public async Task<IActionResult> GetScopes(int id)
        {
            return Json(await _clientService.GetScopesByClientId(id));
        }

        public async Task<IActionResult> UpdateScopes(int id, List<string> scopes)
        {
            return Json(await _clientService.UpdateScope(id, scopes));
        }

        public async Task<IActionResult> DeleteScopes(int id, string scope)
        {
            return Json(await _clientService.DeleteScope(id, scope));
        }


        public async Task<IActionResult> RemoveClient(int id)
        {
            return Json(await _clientService.Delete(id));
        }
    }
}
