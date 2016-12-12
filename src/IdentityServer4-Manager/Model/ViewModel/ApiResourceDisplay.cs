using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class ApiResourceDisplay
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public string DisplayName { set; get; }
        public bool Enabled { set; get; }
        public string Name { set; get; }

        public int ScopeCount { set; get; }
        public int SecretCount { set; get; }
        public int ClaimCount { set; get; }
    }
}
