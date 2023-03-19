using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.CityVMs;
using OnlineBookingTicket.Models.MovieShowVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;

namespace OnlineBookingTicket.Controllers
{
    public class CitiesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public CitiesController(IHttpContextAccessor httpContextAccessor,
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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7099/odata/Cities");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }
            var data = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(data);

            var cities = ((JArray)temp.value).Select(x => new ListCityVM
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();

            return View(cities);
        }
        public async Task<IActionResult> Details(int id)
        {
            apiUrl = "https://localhost:7099/odata/Cities";
            string query = $"({id})";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);

            var movie = new CityVM
            {
                Id = id,
                Name = (string)temp["Name"],
            };

            return View(movie);
        }
    }
}
