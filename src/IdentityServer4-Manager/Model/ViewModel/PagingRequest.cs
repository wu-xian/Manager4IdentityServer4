﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model.ViewModel
{
    public class PagingRequest
    {
        public int Limit { set; get; }
        public int Offset { set; get; }
        public string Order { set; get; }
    }
}
