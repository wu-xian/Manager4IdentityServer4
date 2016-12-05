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

        public async Task<IActionResult> Login()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            TokenClient client = new TokenClient(disco.TokenEndpoint, "client", "secret".Sha256());
            var response = await client.RequestResourceOwnerPasswordAsync("alice", "alice", "api1");
            return Json(response.Json);
        }
    }
}
