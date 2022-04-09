using BLL.Interfaces;
using EnglishWordHelperApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace EnglishWordHelperApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Authenticate([FromBody] UserLoginDto loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("UserLogin object is null");
            }
            var token = await _authService.Authenticate(loginModel.Email, loginModel.Password);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<UserToken>> Refresh(UserToken tokenDto)
        {
            if (tokenDto == null)
            {
                return BadRequest("UserToken object is null");
            }
            var token = await _authService.RefreshAuth(tokenDto);

            return Ok(token);
        }
    }
}
