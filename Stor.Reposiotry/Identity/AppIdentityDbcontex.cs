using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Identity
{
	public class AppIdentityDbcontex :IdentityDbContext<AppUser>
	{
        public AppIdentityDbcontex(DbContextOptions <AppIdentityDbcontex> options) :base(options)
        {
            
        }
    }
}
 