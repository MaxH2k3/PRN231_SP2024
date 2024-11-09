using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using PEPRN231_FA24_TongTranLeHuy_FE.Helper;
using PEPRN231_FA24_TongTranLeHuy_FE.Models;
using System.Net;

namespace UI.Pages.crud
{
    public class DeletePageModel : PageModel
    {
		private readonly FetchService _fetchService;

        public DeletePageModel(FetchService fetchService)
		{
            _fetchService = fetchService;
        }

        public WaterColorsPainting WaterColorsPainting { get; set; } = new WaterColorsPainting();

        public async Task<IActionResult> OnGetAsync(string id)
		{
			if (!HttpContext.Session.TryGetValue("AccessToken", out _))
			{
				return RedirectToPage("/Login");
			}

            var apiResponse = await _fetchService.GetAsync<APIResponse<WaterColorsPainting>>($"api/v1/water-colores-painting/{id}");

            if(apiResponse.StatusResponse != HttpStatusCode.OK)
			{
                ViewData["message"] = "Water color is not existed";
                return RedirectToPage("HomePage");
			}

            WaterColorsPainting = apiResponse.Data!;

            return Page();
		}

		public async Task<IActionResult> OnPostAsync(string id)
		{
			var apiResponse = await _fetchService.DeleteAsync<APIResponse<WaterColorsPainting>>($"api/v1/water-colores-painting/{id}");

            if (apiResponse.StatusResponse != HttpStatusCode.OK)
            {
                ViewData["message"] = apiResponse.Message;

                return Page();
            }

            ViewData["message"] = apiResponse.Message;

            return RedirectToPage("HomePage");
		}
	}
}
