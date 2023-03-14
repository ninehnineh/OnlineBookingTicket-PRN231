using Microsoft.AspNetCore.Mvc;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;

namespace OnlineBookingTicket.Controllers
{
    public class MovieShowsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public MovieShowsController(IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorageService)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _localStorageService = localStorageService;
            var token = new BaseHttpService(_localStorageService, _httpClient);
            token.AddBearerToken();
        }

        public IActionResult BookMovieShow(int movieShowId, int totalSeats)
        {

            return View();
        }

        public ActionResult SelectSeats(int movieShowId, int totalSeats)
        {
            var seats = Enumerable.Range(1, totalSeats);
            ViewBag.MovieShowId = movieShowId;
            return View(seats);
        }
    }
}
