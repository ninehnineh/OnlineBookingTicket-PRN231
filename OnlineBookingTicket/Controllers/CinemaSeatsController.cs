using Microsoft.AspNetCore.Mvc;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;

namespace OnlineBookingTicket.Controllers
{
    public class CinemaSeatsController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private string apiUrl = "";

        public CinemaSeatsController(IHttpContextAccessor httpContextAccessor,
            ILocalStorageService localStorageService)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _localStorageService = localStorageService;
            var token = new BaseHttpService(_localStorageService, _httpClient);
            token.AddBearerToken();
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
