
using WatercolorsPaintingRepository.Entity;

namespace Services
{
	public interface IAuthenticationService
	{
		string? GenerateToken(UserAccount? user);
		Guid GetUserId();
		string GetUserEmail();
		int GetUserRole();
		bool? CheckAuthentication();
	}
}