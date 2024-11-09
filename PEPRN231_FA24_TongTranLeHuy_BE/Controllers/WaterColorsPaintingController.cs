using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PEPRN231_FA24_TongTranLeHuy_BE.Models;
using WatercolorsPaintingRepository.Entity;
using WatercolorsPaintingRepository.Repositories.StyleRepo;
using WatercolorsPaintingRepository.Repositories.WaterColorsPaintingRepo;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Controllers
{
	[ApiController]
	[Route("api/v1/water-colores-painting")]
	public class WaterColorsPaintingController : Controller
	{
		private readonly IWaterColorsPaintingRepository _waterColorsPaintingRepository;
		private readonly IStyleRepository _styleRepository;

		public WaterColorsPaintingController(IWaterColorsPaintingRepository waterColorsPaintingRepository, IStyleRepository styleRepository)
		{
			_waterColorsPaintingRepository = waterColorsPaintingRepository;
			_styleRepository = styleRepository;
		}


		/// <summary>
		/// https://localhost:7061/api/v1/water-colores-painting?$filter=contains(PaintingAuthor, 'co') or PublishYear eq 11
		/// </summary>
		/// <returns></returns>
		[EnableQuery]
		[HttpGet]
		public async Task<IActionResult> GetWaterColors()
		{
			var waterColorsPaintins = await _waterColorsPaintingRepository.GetAll();
			return Ok(waterColorsPaintins);
		}

		[HttpPost]
		public async Task<IActionResult> AddWaterColors([FromBody] WatercolorsPaintingRequest request)
		{
			if(!request.ValidateData(_styleRepository))
			{
				return BadRequest(request.Errors);
			}

			var waterColorsPainting = new WatercolorsPainting
			{
				PaintingId = await _waterColorsPaintingRepository.CreateId(),
				PaintingName = request.PaintingName,
				PaintingDescription = request.PaintingDescription,
				PaintingAuthor = request.PaintingAuthor,
				Price = request.Price,
				PublishYear = request.PublishYear,
				StyleId = request.StyleId,
				CreatedDate = DateTime.Now
			};

			var result = await _waterColorsPaintingRepository.Add(waterColorsPainting);

			if (result)
			{
				return Ok("Create successfully!");
			}

			return BadRequest("Fail to create");
		}
	}
}
