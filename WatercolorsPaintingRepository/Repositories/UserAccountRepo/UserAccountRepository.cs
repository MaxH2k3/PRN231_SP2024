using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.UserAccountRepo
{
	public class UserAccountRepository : IUserAccountRepository
	{
		private readonly WatercolorsPainting2024DBContext _context;

		public UserAccountRepository(WatercolorsPainting2024DBContext context)
		{
			_context = context;
		}

		public UserAccountRepository()
		{
			_context = new WatercolorsPainting2024DBContext();
		}

		public async Task<UserAccount?> Login(string email, string password)
		{
			return await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserEmail!.Equals(email) && u.UserPassword.Equals(password));
		}

	}
}
