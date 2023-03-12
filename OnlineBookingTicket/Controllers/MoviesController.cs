using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;

namespace OnlineBookingTicket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public MoviesController(IHttpContextAccessor httpContextAccessor,
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
            apiUrl = "https://localhost:7099/odata/Movies";
            string query = $"?$select=Title,Image";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }
            var data = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(data);

            var movies = ((JArray)temp.value).Select(x => new ListMovieVM
            {
                Id = (int)x["Id"],
                Title = (string)x["Title"],
                Image = (string)x["Image"]
            }).ToList();

            return View(movies);

        }
    }
}
