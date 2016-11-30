using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Exceptions
{
    public class ParamsWrongException : BaseException
    {
        public ParamsWrongException(string errorMsg) : base(errorMsg)
        {
        }
    }
}
