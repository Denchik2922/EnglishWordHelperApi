using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;

		public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		public async Task<UserToken> RefreshAuth(UserToken userToken)
		{
			var principal = _tokenService.GetPrincipalFromExpiredToken(userToken.Token);
			var username = principal.Identity.Name;

			var user = await _userManager.FindByNameAsync(username);
			if (user == null ||
				user.RefreshToken != userToken.RefreshToken ||
				user.RefreshTokenExpiryTime <= DateTime.Now)
			{
				throw new Exception("Invalid client request");
			}

			var token = await _tokenService.GetToken(user);
			user.RefreshToken = _tokenService.GenerateRefreshToken();

			var result = await _userManager.UpdateAsync(user);
			if (result != IdentityResult.Success)
			{
				foreach (var error in result.Errors)
				{
					throw new Exception($"Code: {error.Code}, Description: { error.Description}");
				}
			}
			return new UserToken { Token = token, RefreshToken = user.RefreshToken };
		}

		public async Task<UserToken> Authenticate(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
			{
				throw new ItemNotFoundException($"{typeof(AppUser).Name} with email {email} not found.");
			}

			if (await _userManager.CheckPasswordAsync(user, password))
			{
				var token = await _tokenService.GetToken(user);
				user.RefreshToken = _tokenService.GenerateRefreshToken();
				user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

				await _userManager.UpdateAsync(user);
				return new UserToken { Token = token, RefreshToken = user.RefreshToken };
			}
			else
			{
				throw new Exception($"Password does not match the user {typeof(AppUser).Name} with email {email}");
			}
		}
	}
}
