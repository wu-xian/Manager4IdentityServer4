using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class ApiResourceViewModel
    {
        public int Id { set; get; }
        public bool Enabled { set; get; } = true;
        public string Name { set; get; }
        public string DisplayName { set; get; }
        public string Description { set; get; }
    }
}
