using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.WaterColorsPaintingRepo
{
	public interface IWaterColorsPaintingRepository
	{
		Task<bool> Add(WatercolorsPainting watercolorsPainting);
		Task<bool> Delete(string id);
		Task<bool> Update(WatercolorsPainting watercolorsPainting);
		Task<IEnumerable<WatercolorsPainting>> GetAll();
		Task<string> CreateId();
		Task<WatercolorsPainting?> GetById(string id);
	}
}
