using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
	public class AppUser : IdentityUser
	{
		public string RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }
	}
}
