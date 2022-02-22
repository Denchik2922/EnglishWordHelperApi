using Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ITokenService
	{
		string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
		Task<string> GetToken(AppUser user);
	}
}