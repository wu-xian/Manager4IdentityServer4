using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Authentication
{
    public class AuthFilter : AuthorizeFilter
    {
        public AuthFilter(AuthorizationPolicy policy) : base(policy)
        {
        }

        public AuthFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData)
            : base(policyProvider, authorizeData)
        {
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Console.WriteLine("#OnAuthorizationAsync#");
            //context.RouteData.
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new { success = "haha" }, new Newtonsoft.Json.JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return base.OnAuthorizationAsync(context);
        }
    }
}
