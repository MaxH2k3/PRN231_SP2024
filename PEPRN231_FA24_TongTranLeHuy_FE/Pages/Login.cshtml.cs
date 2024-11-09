using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using PEPRN231_FA24_TongTranLeHuy_FE.Helper;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OilPaintingArt_TranCongLam.Pages
{
	public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Password { get; set; } = string.Empty;

		private readonly FetchService _fetchService;

		public LoginModel(FetchService fetchService)
        {
			_fetchService = fetchService;
		}

        public void OnGet()
        {
            //HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var apiResponse = await _fetchService.PostAsync<APIResponse<UserLoginResponse>>(
                    "api/v1/authentication/login", 
                    new 
                    {
						Email = Email,
						Password = Password
					}
                );

				if (apiResponse.StatusResponse == HttpStatusCode.OK)
                {
					if (apiResponse.Data.Role == 2 || apiResponse.Data.Role == 3)
                    {
						HttpContext.Session.SetString("UserFullName", apiResponse.Data!.UserFullName);
						HttpContext.Session.SetString("UserEmail", apiResponse.Data.UserEmail!);
						HttpContext.Session.SetInt32("Role", (int)apiResponse.Data.Role!);
						HttpContext.Session.SetString("AccessToken", apiResponse.Data.Token);

						return Redirect("./OilPaintingArts/OilPaintingArtsPages");
                    }
                   
                    else
                    {
                        ViewData["message"] = "You do not have permission to do this function";
                        return Page();
                    }
                }
                else
                {
                    ViewData["message"] = apiResponse.Message;
                    return Page();
                }
            }
        }
    }
}
