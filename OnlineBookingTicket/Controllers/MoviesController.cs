using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.MovieShowVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Services;
using System.Composition.Convention;
using System.Net.Http.Headers;
using System.Text.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OnlineBookingTicket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly IHostingEnvironment _environment;
        private string apiUrl = "";

        public MoviesController(IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorageService,
            IHostingEnvironment environment)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _localStorageService = localStorageService;
            _environment = environment;
            var token = new BaseHttpService(_localStorageService, _httpClient);
            token.AddBearerToken();
        }

        public async Task<IActionResult> Index()
        {
            apiUrl = "https://localhost:7099/odata/Movies";
            string query = $"?$select=Title,Image,Id";

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
                Image = GetImage((int)x["Id"]).Result
            }).ToList();

            return View(movies);

        }

        public async Task<IActionResult> Details(int id)
        {
            apiUrl = "https://localhost:7099/odata/Movies";
            string query = $"({id})";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);

            var movie = new MovieDetailsVM
            {
                Id = id,
                Title = (string)temp["title"],
                Country = (string)temp["country"],
                Description = (string)temp["description"],
                DurationInMinutes = (int)temp["durationInMinutes"],
                Genre = (string)temp["genre"],
                Image = GetImage(id).Result,
                Language = (string)temp["language"],
                ReleaseDate = (DateTime)temp["releaseDate"],
                MovieShows = (List<MovieShowVM>)temp["movieShows"].ToObject<List<MovieShowVM>>(),
            };
            
            return View(movie);
        }

        public async Task<string> GetImage(int id)
        {
            apiUrl = "https://localhost:7099/image";
            string query = $"{id}";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}/{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                //return View();
            }

            var data = await response.Content.ReadAsStringAsync();

            //var a = Path.Combine(Directory.GetCurrentDirectory(), $"..\\OnlineBookingTicketAPI\\UploadFile\\{data}");
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\UploadFile", data);
            return data;
        }
    }
}
