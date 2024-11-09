using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.UserAccountRepo
{
	public interface IUserAccountRepository
	{
		Task<UserAccount?> Login(string email, string password);
	}
}
