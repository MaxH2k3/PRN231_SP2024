using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PEPRN231_FA24_TongTranLeHuy_FE.Helper;
using PEPRN231_FA24_TongTranLeHuy_FE.Models;

namespace UI.Pages.crud
{
    public class HomePageModel : PageModel
    {
        private readonly FetchService _fetchService;

        public HomePageModel(FetchService fetchService)
        {
            _fetchService = fetchService;
        }

        public List<WaterColorsPainting> WaterColorsPaintings { get; set; } = new List<WaterColorsPainting>();

        public string PaintingAuthorSearch { get; set; } = string.Empty;

        public string PublishYearSearch { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string? paintingAuthorSearch, string? publishYearSearch)
        {
            if (!HttpContext.Session.TryGetValue("AccessToken", out _))
            {
                return RedirectToPage("/Login");
            }

            var filters = new List<string>();

            if (!string.IsNullOrEmpty(paintingAuthorSearch?.Trim()))
            {
                filters.Add($"contains(PaintingAuthor, '{paintingAuthorSearch}')");
                PaintingAuthorSearch = paintingAuthorSearch;
            }

            if (!string.IsNullOrEmpty(publishYearSearch?.Trim()) && int.TryParse(publishYearSearch, out _))
            {
                filters.Add($"PublishYear eq {publishYearSearch}");
                PublishYearSearch = publishYearSearch;
            }

            var filterQuery = filters.Any() ? $"?$filter={string.Join(" and ", filters)}" : string.Empty;

            var endpoint = $"api/v1/water-colores-painting{filterQuery}";

            var apiResponse = await _fetchService.GetAsync<List<WaterColorsPainting>>(endpoint);

            if (apiResponse != null)
            {
                WaterColorsPaintings = apiResponse;
            }

            return Page();

        }


        public IActionResult OnGetLogout(string logout)
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
