using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatercolorsPaintingRepository.Repositories.StyleRepo;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Controllers
{
    [ApiController]
    [Route("api/v1/styles")]
    public class StyleController : Controller
    {
        private readonly IStyleRepository _styleRepository;

        public StyleController(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> GetStyles()
        {
            var styles = await _styleRepository.GetAll();
            return Ok(styles);
        }
    }
}
