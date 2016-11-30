using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class RoleDisplay
    {
        public string RoleId { set; get; }
        public string RoleName { set; get; }

        public List<Model.Claim> RoleClaims { set; get; }
        public List<Model.RoleUser> RoleUsers { set; get; }
    }
}
