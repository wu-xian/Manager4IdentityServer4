using IdentityServer4_Manager.Model.ViewModel;
using IdentityServer4_Manager.Services;
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

        public async Task<IActionResult> AddClientView()
        {
            return PartialView("AddClient");
        }

        public async Task<IActionResult> GetClients(PagingRequest request, string clientId, string clientName)
        {
            return Json(_clientService.GetPaged(request, clientId, clientName));
        }

        public async Task<IActionResult> Get(int id)
        {
            var client = _clientService.Get(id);
            return PartialView("Get", client);
        }

        public async Task<IActionResult> Create(string clientName, string clientUri)
        {
            _clientService.AddClient(clientName, clientUri);
            return Content("123");
        }

        public async Task<IActionResult> RemoveClient(int id)
        {
           await  _clientService.RemoveClient(id);
            return Content("asd");
        }
    }
}
