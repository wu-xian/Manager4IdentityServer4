using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Manager.Model.ViewModel
{
    public class PagingResponse
    {
        public object Rows { set; get; }
        public int Total { set; get; }
    }
}
