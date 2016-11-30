using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Exceptions
{
    public class BaseException : Exception
    {
        public virtual new Dictionary<string, string> Data { set; get; }

        protected BaseException(string errorMsg)
        {
            Data = new Dictionary<string, string>();
            Data.Add("message", errorMsg);
        }
    }
}
