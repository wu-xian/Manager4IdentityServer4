﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Model.ViewModel
{
    public class IdentityResourceViewModel
    {
        public int Id { set; get; }
        public bool Enabled { set; get; } = true;
        public string Name { set; get; }
        public string DisplayName { set; get; }
        public string Description { set; get; }
        public bool Required { set; get; } = false;
        public bool Emphasize { set; get; } = true;
        public bool ShowInDiscoveryDocument { set; get; } = true;
    }
}
