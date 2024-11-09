using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Newtonsoft.Json.Linq;
using PEPRN231_FA24_TongTranLeHuy_FE.Helper;
using PEPRN231_FA24_TongTranLeHuy_FE.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UI.Pages.crud
{
	public class UpdatePageModel : PageModel
    {
		private readonly FetchService _fetchService;

		public UpdatePageModel(FetchService fetchService)
		{
			_fetchService = fetchService;
		}

		public string WaterColorsPaintingId { get; set; } = string.Empty;

        [BindProperty]
		public WatercolorsPaintingRequest WaterColorsPainting { get; set; } = new WatercolorsPaintingRequest();

        public WatercolorsPaintingError Errors { get; set; } = new WatercolorsPaintingError();

        public async Task<IActionResult> OnGetAsync(string? id)
		{
			if(id == null)
			{
                return RedirectToPage("HomePage");
            }

            WaterColorsPaintingId = id;

            if (!HttpContext.Session.TryGetValue("AccessToken", out _))
			{
				return RedirectToPage("/Login");
			}

			var styles = await _fetchService.GetAsync<IEnumerable<Style>>("/api/v1/styles");
			ViewData["StyleIds"] = new SelectList(styles, "StyleId", "StyleName");

			var apiResponse = await _fetchService.GetAsync<APIResponse<WaterColorsPainting>>($"/api/v1/water-colores-painting/{id}");

			if(apiResponse.StatusResponse != System.Net.HttpStatusCode.OK)
			{
				ViewData["Message"] = apiResponse.Message;
				return RedirectToPage("HomePage");
			}

			WaterColorsPainting = new WatercolorsPaintingRequest()
			{
				PaintingName = apiResponse.Data!.PaintingName,
                StyleId = apiResponse.Data!.StyleId,
                PaintingAuthor = apiResponse.Data!.PaintingAuthor,
                PaintingDescription = apiResponse.Data!.PaintingDescription,
                Price = apiResponse.Data!.Price,
				PublishYear = apiResponse.Data!.PublishYear
            };

			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync(string? id)
		{
            var apiResponse = await _fetchService.PutAsync<APIResponse>($"/api/v1/water-colores-painting/{id}", WaterColorsPainting);

            if (apiResponse.StatusResponse == HttpStatusCode.BadRequest)
            {
                Errors = ((JObject)apiResponse.Data!).ToObject<WatercolorsPaintingError>()!;
                ViewData["Message"] = apiResponse.Message;
                var styles = await _fetchService.GetAsync<IEnumerable<Style>>("/api/v1/styles");
                ViewData["StyleIds"] = new SelectList(styles, "StyleId", "StyleName");

                return Page();
            }

            ViewData["Message"] = "Update Successfully";

            return RedirectToPage("HomePage");
        }
    }
}
