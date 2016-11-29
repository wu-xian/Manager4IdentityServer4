using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Manager.Model
{
    public class IdentityRole :
        Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<
            int,
            Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>,
            Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>
            >
    {
    }
}
