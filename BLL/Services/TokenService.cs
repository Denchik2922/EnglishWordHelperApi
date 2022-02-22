using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly string _jwtSecret;
		private readonly string _jwtValidIssuer;
		private readonly string _jwtExpiryInMinutes;
		private readonly string _jwtValidAudience;

		public TokenService(IConfiguration config, UserManager<AppUser> userManager)
		{
			var _jwtSettings = config.GetSection("JwtSettings");

			_jwtSecret = _jwtSettings["Secret"];
			_jwtValidIssuer = _jwtSettings["validIssuer"];
			_jwtValidAudience = _jwtSettings["validAudience"];
			_jwtExpiryInMinutes = _jwtSettings["expiryInMinutes"];

			_userManager = userManager;
		}

		public async Task<string> GetToken(AppUser user)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims(user);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_jwtSecret);
			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private async Task<List<Claim>> GetClaims(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};

			var roles = await _userManager.GetRolesAsync(user);

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				issuer: _jwtValidIssuer,
				audience: _jwtValidAudience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtExpiryInMinutes)),
				signingCredentials: signingCredentials);

			return tokenOptions;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			SecurityToken securityToken;

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(_jwtSecret)),
				ValidateLifetime = false,
				ValidIssuer = _jwtValidIssuer,
				ValidAudience = _jwtValidAudience,
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

			var jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}

			return principal;
		}
	}
}
