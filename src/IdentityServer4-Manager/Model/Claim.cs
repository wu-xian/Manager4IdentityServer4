using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model
{
    public class Claim
    {
        public string ClaimType { set; get; }
        public string ClaimValue { set; get; }
    }
}
