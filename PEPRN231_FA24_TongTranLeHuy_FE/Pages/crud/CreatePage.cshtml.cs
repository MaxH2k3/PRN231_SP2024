using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Newtonsoft.Json.Linq;
using PEPRN231_FA24_TongTranLeHuy_FE.Helper;
using PEPRN231_FA24_TongTranLeHuy_FE.Models;
using System.Net;

namespace UI.Pages.crud
{
    public class CreatePageModel : PageModel
    {
		private readonly FetchService _fetchService;

        public CreatePageModel(FetchService fetchService)
		{
			_fetchService = fetchService;
        }

		[BindProperty]
		public WatercolorsPaintingRequest WaterColorsPainting { get; set; } = new WatercolorsPaintingRequest();

		public WatercolorsPaintingError Errors { get; set; } = new WatercolorsPaintingError();

		public async Task<IActionResult> OnGet()
		{
			if (!HttpContext.Session.TryGetValue("AccessToken", out _))
			{
				return RedirectToPage("/Login");
			}

			var styles = await _fetchService.GetAsync<IEnumerable<Style>>("/api/v1/styles");
			ViewData["StyleIds"] = new SelectList(styles, "StyleId", "StyleName");
			return Page();
		}


		public async Task<IActionResult> OnPostAsync()
		{

			var apiResponse = await _fetchService.PostAsync<APIResponse>("/api/v1/water-colores-painting", WaterColorsPainting);

			if(apiResponse.StatusResponse == HttpStatusCode.BadRequest)
			{
				Errors = ((JObject)apiResponse.Data!).ToObject<WatercolorsPaintingError>()!;
				ViewData["Message"] = apiResponse.Message;
				var styles = await _fetchService.GetAsync<IEnumerable<Style>>("/api/v1/styles");
				ViewData["StyleIds"] = new SelectList(styles, "StyleId", "StyleName");

				return Page();
			}

			ViewData["Message"] = "Create successfully";

			return RedirectToPage("HomePage");
		}
	}
}
