using Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAuthService
	{
		Task<UserToken> Authenticate(string email, string password);
		Task<UserToken> RefreshAuth(UserToken userToken);
	}
}