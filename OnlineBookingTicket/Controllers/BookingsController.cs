using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineBookingTicket.Controllers
{
    public class BookingsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public BookingsController(IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorageService)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _localStorageService = localStorageService;
            var token = new BaseHttpService(_localStorageService, _httpClient);
            token.AddBearerToken();
        }

        public async Task<IActionResult> Index()
        {
            apiUrl = "https://localhost:7099/getbookingsbyUserId/";
            string query = $"{GetAuthenticatedUserId()}";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var bookings = JsonSerializer.Deserialize<IEnumerable<BookingVM>>(data, options);

            return View(bookings);
        }
        private string GetAuthenticatedUserId()
        {
            var authenticatedUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid").ToString();

            var userId = authenticatedUser.Contains("uid") ?
                authenticatedUser.Substring(authenticatedUser.IndexOf(":") + 1).TrimStart() : authenticatedUser;
            return userId;
        }
    }
}
