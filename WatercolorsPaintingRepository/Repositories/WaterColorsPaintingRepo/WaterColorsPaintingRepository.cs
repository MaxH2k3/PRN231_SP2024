using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.WaterColorsPaintingRepo
{
	public class WaterColorsPaintingRepository : IWaterColorsPaintingRepository
	{
		private readonly WatercolorsPainting2024DBContext _context;

		public WaterColorsPaintingRepository(WatercolorsPainting2024DBContext context)
		{
			_context = context;
		}

		public async Task<bool> Add(WatercolorsPainting watercolorsPainting)
		{
			await _context.AddAsync(watercolorsPainting);
		
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Delete(string id)
		{
			var watercolorsPainting = await _context.WatercolorsPaintings.FirstOrDefaultAsync(w => w.PaintingId.Equals(id));
			if (watercolorsPainting == null)
			{
				return false;
			}

			_context.WatercolorsPaintings.Remove(watercolorsPainting);

			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Update(WatercolorsPainting watercolorsPainting)
		{
			_context.WatercolorsPaintings.Update(watercolorsPainting);

			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<WatercolorsPainting>> GetAll()
		{
			return await _context.WatercolorsPaintings
				.Include(p => p.Style)
				.AsQueryable()
				.ToListAsync();
		}

		public async Task<string> CreateId()
		{
			var maxId = await _context.WatercolorsPaintings
				.OrderByDescending(p => p.PaintingId)
				.Select(p => p.PaintingId)
				.FirstOrDefaultAsync();

			if (string.IsNullOrEmpty(maxId))
			{
				return "P001";
			}

			var numericPart = int.Parse(maxId.Substring(1));

			var nextNumericPart = numericPart + 1;

			// Create ID with format P###
			return $"P{nextNumericPart:D3}";
		}

	}
}
