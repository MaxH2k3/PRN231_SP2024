using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatercolorsPaintingRepository.Entity;

namespace WatercolorsPaintingRepository.Repositories.StyleRepo
{
	public interface IStyleRepository
	{
		bool IsExist(string id);
		Task<IEnumerable<Style>> GetAll();

    }
}
