using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Models;
using PEPRN231_FA24_TongTranLeHuy_BE.Models;
using System.Net;
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

		public async Task<IActionResult> GetWaterColorsById(string id)
		{
			var waterColorsPainting = await _waterColorsPaintingRepository.GetById(id);

			if (waterColorsPainting == null)
			{
				return NotFound(new APIResponse()
				{
					StatusResponse = HttpStatusCode.NotFound,
					Message = "Not found"
				});
			}

			return Ok(new APIResponse()
			{
				StatusResponse = HttpStatusCode.OK,
				Message = "Success",
				Data = waterColorsPainting
			});
		}

		[HttpPost]
		public async Task<IActionResult> AddWaterColors([FromBody] WatercolorsPaintingRequest request)
		{
			if(!request.ValidateData(_styleRepository))
			{
				return BadRequest(new APIResponse()
				{
					StatusResponse = HttpStatusCode.BadRequest,
					Message = "Invalid data",
					Data = request.Errors
				});
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
				return Ok(new APIResponse()
				{
					StatusResponse = HttpStatusCode.Created,
					Message = "Create successfully",
					Data = waterColorsPainting.PaintingId
				});
			}

			return StatusCode(500, new APIResponse()
			{
				StatusResponse = HttpStatusCode.InternalServerError,
				Message = "Fail to create"
			});
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateWaterColors(string id, [FromBody] WatercolorsPaintingRequest request)
		{
			if (!request.ValidateData(_styleRepository))
			{
				return BadRequest(new APIResponse()
				{
					StatusResponse = HttpStatusCode.BadRequest,
					Message = "Invalid data",
					Data = request.Errors
				});
			}

			var waterColorsPainting = new WatercolorsPainting
			{
				PaintingId = id,
				PaintingName = request.PaintingName,
				PaintingDescription = request.PaintingDescription,
				PaintingAuthor = request.PaintingAuthor,
				Price = request.Price,
				PublishYear = request.PublishYear,
				StyleId = request.StyleId,
				CreatedDate = DateTime.Now
			};

			var result = await _waterColorsPaintingRepository.Update(waterColorsPainting);

			if (result)
			{
				return Ok(new APIResponse()
				{
					StatusResponse = HttpStatusCode.OK,
					Message = "Update successfully",
					Data = waterColorsPainting.PaintingId
				});
			}

			return StatusCode(500, new APIResponse()
			{
				StatusResponse = HttpStatusCode.InternalServerError,
				Message = "Fail to update"
			});
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteWaterColors(string id)
		{
			var result = await _waterColorsPaintingRepository.Delete(id);

			if (result)
			{
				return Ok(new APIResponse()
				{
					StatusResponse = HttpStatusCode.OK,
					Message = "Delete successfully",
				});
			}

			return StatusCode(500, new APIResponse()
			{
				StatusResponse = HttpStatusCode.InternalServerError,
				Message = "Fail to delete"
			});
		}
	}
}
