using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatercolorsPaintingRepository.Repositories.StyleRepo
{
	public interface IStyleRepository
	{
		bool IsExist(string id);
	}
}
