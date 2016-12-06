using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        public async Task<IActionResult> Login(string clientId, string userName, string password)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:9090");
            TokenClient client = new TokenClient(disco.TokenEndpoint, clientId);
            var response = await client.RequestResourceOwnerPasswordAsync(userName, password, "MVC.ADMIN");
            return Json(response.Json);
        }
    }
}
