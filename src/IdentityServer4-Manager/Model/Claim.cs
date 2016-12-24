using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Model
{
    public class Claim
    {
        public string Id { set; get; }
        public string ClaimType { set; get; }
        public string ClaimValue { set; get; }
    }
}
