using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.Identity;
using System.Security.Claims;

namespace Store.APIs.Extensions
{
	public static class UserMAngerExtention
	{
		public static async Task<AppUser?> FindUserWithAdressAsync (this UserManager<AppUser> userManager, ClaimsPrincipal claims)
		{
			var email = claims.FindFirstValue (ClaimTypes.Email);
			var user = await userManager.Users.Include(U=>U.address).FirstOrDefaultAsync (u => u.Email == email);
			return user;
		}
	}
}
