using BusinessObject.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Models.MovieShowVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Services;
using System.Composition.Convention;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            //var a = await GetBookingAsync(GetAuthenticatedUserId());
            //ViewBag.UserID = GetAuthenticatedUserId();
            //ViewBag.MovieShowID = a.MovieShowID;
            return View(movie);
        }

        private string GetAuthenticatedUserId()
        {
            var authenticatedUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid").ToString();

            var userId = authenticatedUser.Contains("uid") ?
                authenticatedUser.Substring(authenticatedUser.IndexOf(":") + 1).TrimStart() : authenticatedUser;
            return userId;
        }

        public async Task<string> GetImage(int id)
        {
            apiUrl = "https://localhost:7099/image";
            string query = $"{id}";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}/{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
            }

            var data = await response.Content.ReadAsStringAsync();

            return data;
        }

        private async Task<BookingVM> GetBookingAsync(string userId)
        {
            apiUrl = "https://localhost:7099/odata/Bookings?userId=";
            string query = $"{userId}";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                //return View();
            }

            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var booking = JsonSerializer.Deserialize<BookingVM>(data, options);

            return booking;
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        //public async Task<HttpResponseMessage> CreateMovie(IFormFile file, CreateMovieVM viewModel)
        //{
        //    using (var content = new MultipartFormDataContent())
        //    {
        //        // Thêm thông tin về phim vào request
        //        content.Add(new StringContent(viewModel.Title), "title");
        //        content.Add(new StringContent(viewModel.Description), "description");
        //        content.Add(new StringContent(viewModel.DurationInMinutes.ToString()), "durationInMinutes");
        //        content.Add(new StringContent(viewModel.Language), "language");
        //        content.Add(new StringContent(viewModel.ReleaseDate.ToString("yyyy-MM-dd")), "releaseDate");
        //        content.Add(new StringContent(viewModel.Country), "country");
        //        content.Add(new StringContent(viewModel.Genre), "genre");

        //        // Thêm file vào request
        //        if (file != null && file.Length > 0)
        //        {
        //            var fileContent = new StreamContent(file.OpenReadStream());
        //            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        //            content.Add(fileContent, "image", file.FileName);
        //        }

        //        // Gửi request đến API để tạo mới phim
        //        return await httpClient.PostAsync("api/movies", content);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> Create(IFormFile file, CreateMovieVM movieVM)
        {
            apiUrl = "https://localhost:7099/odata/Movies";

            if (ModelState.IsValid)
            {

                using (var content = new MultipartFormDataContent())
                {

                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    content.Add(fileContent, "file", file.FileName);

                    content.Add(new StringContent(movieVM.Title), "title");
                    content.Add(new StringContent(movieVM.Description), "description");
                    content.Add(new StringContent(movieVM.DurationInMinutes.ToString()), "durationInMinutes");
                    content.Add(new StringContent(movieVM.Language), "language");
                    content.Add(new StringContent(movieVM.ReleaseDate.ToString("yyyy-MM-dd")), "releaseDate");
                    content.Add(new StringContent(movieVM.Country), "country");
                    content.Add(new StringContent(movieVM.Genre), "genre");
                    
                    string jsonContent = JsonSerializer.Serialize(movieVM);
                    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    content.Add(stringContent, "movieVM");


                    HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Insert successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View();

        }
    }
}
