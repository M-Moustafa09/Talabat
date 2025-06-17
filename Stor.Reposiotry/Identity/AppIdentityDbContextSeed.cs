using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeeduserAsync(UserManager<AppUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var User = new AppUser()
				{
					Displayname = "Mahmoud Mustafa",
					Email = "mahmoud.gmail.com",
					UserName = "mahmoud.gmail",
					PhoneNumber = "01147474365"
				};
				await userManager.CreateAsync(User, "Pa$$w0rd");

			}
			
		}

	}
}
