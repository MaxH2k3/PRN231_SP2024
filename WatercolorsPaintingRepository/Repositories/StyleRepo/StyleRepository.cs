using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.StyleRepo
{
	public class StyleRepository : IStyleRepository
	{
		private readonly WatercolorsPainting2024DBContext _context;

		public StyleRepository(WatercolorsPainting2024DBContext context)
		{
			_context = context;
		}

		public bool IsExist(string id)
		{
			return _context.Styles.Any(s => s.StyleId.Equals(id));
		}

		public async Task<IEnumerable<Style>> GetAll()
		{
			return await _context.Styles.AsQueryable()
                .ToListAsync();
        }

	}
}
