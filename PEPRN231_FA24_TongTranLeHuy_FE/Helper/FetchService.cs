using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace PEPRN231_FA24_TongTranLeHuy_FE.Helper
{

	public class FetchService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private static string _baseUrl = "https://localhost:7061";

		public FetchService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClient;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<T?> SendAsync<T>(HttpMethod method, string endpoint, object? data = null)
		{
			var url = $"{_baseUrl}/{endpoint.TrimStart('/')}";

			// Tạo request chung
			using var request = new HttpRequestMessage(method, url);

			// Gắn dữ liệu nếu có
			if (data != null)
			{
				request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
			}

			// Thêm Authorization header nếu có token
			var token = _httpContextAccessor.HttpContext?.Session.GetString("accessToken");
			if (!string.IsNullOrEmpty(token) && (token != null))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}

			// Gửi yêu cầu
			using var response = await _httpClient.SendAsync(request);

			// Xử lý phản hồi
			//if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Unauthorized) return default;

			var responseStream = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(responseStream);
		}

		public Task<T?> GetAsync<T>(string endpoint) => SendAsync<T>(HttpMethod.Get, endpoint);

		public Task<T?> PostAsync<T>(string endpoint, object data) => SendAsync<T>(HttpMethod.Post, endpoint, data);

		public Task<T?> PutAsync<T>(string endpoint, object data) => SendAsync<T>(HttpMethod.Put, endpoint, data);

		public Task<T?> DeleteAsync<T>(string endpoint) => SendAsync<T>(HttpMethod.Delete, endpoint);
	}

}
