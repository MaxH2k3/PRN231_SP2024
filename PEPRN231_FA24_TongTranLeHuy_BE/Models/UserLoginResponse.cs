﻿namespace PEPRN231_FA24_TongTranLeHuy_BE.Models
{
	public class UserLoginResponse
	{
		public string UserFullName { get; set; } = null!;

		public string? UserEmail { get; set; }

		public int? Role { get; set; }

		public string Token { get; set; } = null!;
	}
}
