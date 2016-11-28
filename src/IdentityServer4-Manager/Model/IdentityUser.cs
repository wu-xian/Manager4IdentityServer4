using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model
{
    public class IdentityUser : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser<Guid>
    {
    }
}
