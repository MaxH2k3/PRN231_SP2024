using Constants;
using Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WatercolorsPaintingRepository.Entity;

namespace Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly JWTSetting _jwtsetting;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthenticationService(IOptions<JWTSetting> jwtsetting, IHttpContextAccessor httpContextAccessor)
		{
			_jwtsetting = jwtsetting.Value;
			_httpContextAccessor = httpContextAccessor;
		}

		public string? GenerateToken(UserAccount? user)
		{
			if (user == null)
			{
				return null;
			}

			var tokenhandler = new JwtSecurityTokenHandler();
			var tokenkey = Encoding.UTF8.GetBytes(_jwtsetting.SecurityKey!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(
					new Claim[]
					{
						new Claim(UserClaimType.UserId, user.UserAccountId.ToString()),
						new Claim(ClaimTypes.Role, user.Role.ToString()!)
					}
				),
				Expires = DateTime.Now.AddDays((double)_jwtsetting.TokenExpiry!),
				Issuer = _jwtsetting.Issuer,
				Audience = _jwtsetting.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
			};
			var token = tokenhandler.CreateToken(tokenDescriptor);
			string finaltoken = tokenhandler.WriteToken(token);

			return finaltoken;
		}

		public Guid GetUserId()
		{
			return _httpContextAccessor.HttpContext?.User.GetUserIdFromToken() ?? throw new Exception("Unauthorize!");
		}

		public string GetUserEmail()
		{
			return _httpContextAccessor.HttpContext?.User.GetEmailFromToken() ?? throw new Exception("Unauthorize!");
		}

		public int GetUserRole()
		{
			return _httpContextAccessor.HttpContext?.User.GetRoleFromToken() ?? throw new Exception("Unauthorize!");
		}

		public bool? CheckAuthentication()
		{
			return _httpContextAccessor.HttpContext?.User.Identity!.IsAuthenticated;
		}
	}
}
