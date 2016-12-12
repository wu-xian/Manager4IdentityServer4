using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class IdentityResourceDisplay
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public string DisplayName { set; get; }
        public bool Enabled { set; get; }
        public string Name { set; get; }
        public bool Required { set; get; }

        public int ClaimCount { set; get; }
    }
}
