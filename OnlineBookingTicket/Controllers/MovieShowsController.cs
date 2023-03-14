using Microsoft.AspNetCore.Mvc;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Models.CinemaSeatVMs;
using OnlineBookingTicket.Services;
using System.Net.Http.Headers;
using System.Text.Json;

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


        public ActionResult SelectSeats(int movieShowId, int totalSeats, int cinemaHallId)
        {
            var seats = Enumerable.Range(1, totalSeats);
            ViewBag.MovieShowId = movieShowId;
            ViewBag.cinemaHallId = cinemaHallId;
            return View(seats);
        }
        public async Task<IActionResult> BookMovieShow(string[] seatSelection, int movieShowId, int cinemaHallId)
        {

            foreach (var seat in seatSelection)
            {
                var cinemaSeat = new CreateCinemaSeatVM
                {
                    SeatNumber = (int)Int32.Parse(seat),
                    Type = 1,
                    CinemaHallID = cinemaHallId,
                };
                await CreateCinemaSeats(cinemaSeat);
            }

            string userId = GetAuthenticatedUserId();

            var bookingVM = new CreateBookingVM
            {
                AppUserID = userId,
                MovieShowID = movieShowId,
                NumberOfSeats = seatSelection.Length,
                Timestamp = DateTime.Now,
            };

            await CreateBooking(bookingVM);



            return View();
        }
        private async Task<IActionResult> CreateCinemaSeats(CreateCinemaSeatVM cinemaSeat)
        {
            apiUrl = "https://localhost:7099/odata/CinemaSeats";

            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(cinemaSeat);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            return View();
        }

        private string GetAuthenticatedUserId()
        {
            var authenticatedUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid").ToString();

            var userId = authenticatedUser.Contains("uid") ?
                authenticatedUser.Substring(authenticatedUser.IndexOf(":") + 1).TrimStart() : authenticatedUser;
            return userId;
        }

        private async Task<ActionResult> CreateBooking(CreateBookingVM bookingVM)
        {
            apiUrl = "https://localhost:7099/odata/Bookings";

            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(bookingVM);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, contentData);

                var data = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var bookingId = JsonSerializer.Deserialize<CreateBookingResponseVM>(data, options).Id;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }

            return View();

        }
    }
}
