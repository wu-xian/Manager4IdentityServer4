using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Model.ViewModel
{
    public class UserDisplay
    {
        public string Id { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }

        public List<Model.UserRole> UserRoles { set; get; }
        public List<Model.Claim> UserClaims { set; get; }
    }
}
