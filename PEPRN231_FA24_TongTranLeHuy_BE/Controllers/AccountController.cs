﻿using Microsoft.AspNetCore.Mvc;
using Models;
using PEPRN231_FA24_TongTranLeHuy_BE.Models;
using Services;
using System.Net;
using WatercolorsPaintingRepository.Repositories.UserAccountRepo;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Controllers
{
	[ApiController]
	[Route("api/v1/authentication")]
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
				return BadRequest(new APIResponse()
				{
					StatusResponse = HttpStatusCode.BadRequest,
					Message = "Email or password is incorrect"
				});
			}

			var response = new UserLoginResponse
			{
				UserFullName = user!.UserFullName,
				UserEmail = user.UserEmail,
				Role = user.Role,
				Token = _authenticationService.GenerateToken(user) ?? ""
			};

			return Ok(new APIResponse()
			{
				StatusResponse = HttpStatusCode.OK,
				Message = "Login successfully",
				Data = response
			});
		}
	}
}
