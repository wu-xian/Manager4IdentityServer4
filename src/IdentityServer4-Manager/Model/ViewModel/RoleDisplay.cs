﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Model.ViewModel
{
    public class RoleDisplay
    {
        public string RoleId { set; get; }
        public string RoleName { set; get; }
        public string NormalizeName { set; get; }

        public int ClaimCount { set; get; }
        public int UserCount { set; get; }
    }
}
