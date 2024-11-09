using Microsoft.AspNetCore.Mvc;
using PEPRN231_FA24_TongTranLeHuy_BE.Models;
using Services;
using WatercolorsPaintingRepository.Repositories.UserAccountRepo;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Controllers
{
	[ApiController]
	public class AccountController : Controller
	{
		private readonly IUserAccountRepository _userAccountRepository;
		private readonly IAuthenticationService _authenticationService;

		public AccountController(IUserAccountRepository userAccountRepository, IAuthenticationService authenticationService)
		{
			_userAccountRepository = userAccountRepository;
			_authenticationService = authenticationService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UserLoginRequest request)
		{
			var user = await _userAccountRepository.Login(request.Email, request.Password);

			if(user == null)
			{
				return BadRequest("Invalid email or password");
			}

			var response = new UserLoginResponse
			{
				UserFullName = user!.UserFullName,
				UserEmail = user.UserEmail,
				Role = user.Role,
				Token = _authenticationService.GenerateToken(user) ?? ""
			};

			return Ok(response);
		}
	}
}
