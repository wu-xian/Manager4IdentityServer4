using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class LoginViewModel
    {
        [MaxLength(10)]
        [Required]
        public string UserName { set; get; }
        [MaxLength(20)]
        [Required]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}
