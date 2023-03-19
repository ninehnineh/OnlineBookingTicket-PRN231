using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBookingTicket.Contracts;
using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Models.CinemaHallVMs;
using OnlineBookingTicket.Models.CinemaSeatVMs;
using OnlineBookingTicket.Models.MovieShowVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Models.ShowSeatVMs;
using OnlineBookingTicket.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        public async Task<IActionResult> Index()
        {
            apiUrl = "https://localhost:7099/odata/MovieShows";
            string query = $"?$select=Date,Starttime,Endtime,MovieID,CinemaID,Id";

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7099/odata/MovieShows");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }
            var data = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(data);

            var movieShows = ((JArray)temp.value).Select(x => new ListMovieShowVM
            {
                Id = (int)x["Id"],
                Date = (DateTime)x["Date"],
                Starttime = (DateTime)x["Starttime"],
                Endtime = (DateTime)x["Endtime"],
                CinemaHallID = (int)x["CinemaHallID"],
                MovieID = (int)x["MovieID"],

            }).ToList();
            HttpResponseMessage responses = await _httpClient.GetAsync("https://localhost:7099/odata/Movies");
            string strData = await responses.Content.ReadAsStringAsync();


            dynamic tempss = JObject.Parse(strData);

            var movies = ((JArray)tempss.value).Select(x => new ListMovieVM
            {
                Id = (int)x["Id"],
                Title = (string)x["Title"],
            }).ToList();

            ViewBag.Movies = movies;


            HttpResponseMessage responseMessages = await _httpClient.GetAsync("https://localhost:7099/odata/CinemaHalls");
            string stringDatas = await responseMessages.Content.ReadAsStringAsync();
            JToken token = JToken.Parse(stringDatas);
            JArray cinemaHallAray = (JArray)token.SelectToken("value");
            List<CinemaHallVM> cinemaHalls = new List<CinemaHallVM>();
            foreach (JToken cinemaHall in cinemaHallAray)
            {
                CinemaHallVM cinemaHallVM = new CinemaHallVM
                {
                    Name = (string)cinemaHall["Name"],
                };
                cinemaHalls.Add(cinemaHallVM);
            }

            ViewBag.CinemaHalls = cinemaHalls;  

            return View(movieShows);

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
            var cinemaSeatsId = new List<CreateCinemaSeatResponseVM>();

            foreach (var seat in seatSelection)
            {
                var cinemaSeat = new CreateCinemaSeatVM
                {
                    SeatNumber = (int)Int32.Parse(seat),
                    Type = 1,
                    CinemaHallID = cinemaHallId,
                };
                cinemaSeatsId.Add(CreateCinemaSeats(cinemaSeat).Result.Value);
            }
            
            string userId = GetAuthenticatedUserId();

            var bookingVM = new CreateBookingVM
            {
                AppUserID = userId,
                MovieShowID = movieShowId,
                NumberOfSeats = seatSelection.Length,
                Timestamp = DateTime.Now,
            };

            var bookingId = CreateBooking(bookingVM).Result.Value.Id;


            foreach (var cinemaSeat in cinemaSeatsId)
            {
                var showSeat = new CreateShowSeatVM
                {
                    BookingID = bookingId,
                    CinemaSeatID = cinemaSeat.Id,
                    MovieShowID = movieShowId,
                    Status = Models.Enum.ShowSeatStatusVM.Booked,
                    Price = 100
                };

                await CreateShowSeat(showSeat);
            }

            return View();
        }
        private async Task<ActionResult<CreateCinemaSeatResponseVM>> CreateCinemaSeats(CreateCinemaSeatVM cinemaSeat)
        {
            apiUrl = "https://localhost:7099/odata/CinemaSeats";

            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(cinemaSeat);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, contentData);

                var data = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var cinemaSeatId = JsonSerializer.Deserialize<CreateCinemaSeatResponseVM>(data, options).Id;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                    return new CreateCinemaSeatResponseVM { Id = cinemaSeatId };       
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

        private async Task<ActionResult<CreateBookingResponseVM>> CreateBooking(CreateBookingVM bookingVM)
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
                    return new CreateBookingResponseVM { Id = bookingId };
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }

            return View();

        }

        private async Task<ActionResult> CreateShowSeat(CreateShowSeatVM showSeatVM)
        {
            apiUrl = "https://localhost:7099/odata/ShowSeats";

            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(showSeatVM);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }

            return View();
        }

        public async Task<ActionResult> CreateMovieShow()
        {


            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7099/odata/Movies");
            string strData = await response.Content.ReadAsStringAsync();


            dynamic temp = JObject.Parse(strData);

            var movies = ((JArray)temp.value).Select(x => new ListMovieVM
            {
                Id = (int)x["Id"],
                Title = (string)x["Title"],
                DurationInMinutes = (int)x["DurationInMinutes"],

            }).ToList();

            var titleList = new SelectList(movies, "Id", "Title");
            var durationList = new SelectList(movies, "Id", "DurationInMinutes");

            ViewData["TitleList"] = titleList;
            ViewData["DurationList"] = durationList;



            HttpResponseMessage responseMessages = await _httpClient.GetAsync("https://localhost:7099/odata/CinemaHalls");
            string stringDatas = await responseMessages.Content.ReadAsStringAsync();
            dynamic temps = JObject.Parse(stringDatas);

            var cinemaHalls = ((JArray)temps.value).Select(x => new ListCinemaHallVM
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();

            ViewData["CinemaHallID"] = new SelectList(cinemaHalls, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMovieShow([Bind("Id,Date,Starttime,Endtime,MovieID,CinemaHallID")] CreateMovieShowVM createMovieShowVM)
        {
            HttpResponseMessage responses = await _httpClient.GetAsync("https://localhost:7099/odata/Movies");
            string strDatas = await responses.Content.ReadAsStringAsync();


            dynamic temp = JObject.Parse(strDatas);

            var movies = ((JArray)temp.value).Select(x => new ListMovieVM
            {
                Id = (int)x["Id"],
                Title = (string)x["Title"],
                DurationInMinutes = (int)x["DurationInMinutes"],
            }).ToList();

            apiUrl = "https://localhost:7099/odata/MovieShows";
            if (ModelState.IsValid)
            {
                var movie = movies.FirstOrDefault(x => x.Id == createMovieShowVM.MovieID);
                createMovieShowVM.Endtime.AddMinutes(createMovieShowVM.Starttime.Minute + movie.DurationInMinutes);
                string strData = JsonSerializer.Serialize(createMovieShowVM);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, contentData);
                int time = DateTime.Compare(createMovieShowVM.Endtime,createMovieShowVM.Starttime);
                int result = createMovieShowVM.Endtime.Hour - createMovieShowVM.Starttime.Hour; 
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                    return RedirectToAction(nameof(Index));

                }
                else if(time == 0)
                {
                    ViewBag.Message = "Error start is the same time as endtime";
                }
                else if(time < 0)
                {
                    ViewBag.Message = "Error start is earlier than endtime";
                }else if(result <= 1)
                {
                    ViewBag.Message = "Too Short";
                }    
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            #region Movie List
          
            ViewData["Duration"] = new SelectList(movies, "Id", "DurationInMinutes");
            ViewData["MovieID"] = new SelectList(movies, "Id", "Title");

            #endregion


            HttpResponseMessage responseMessages = await _httpClient.GetAsync("https://localhost:7099/odata/CinemaHalls");
            string stringDatas = await responseMessages.Content.ReadAsStringAsync();
            dynamic temps = JObject.Parse(stringDatas);

            var cinemaHalls = ((JArray)temps.value).Select(x => new ListCinemaHallVM
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();

            ViewData["CinemaHallID"] = new SelectList(cinemaHalls, "Id", "Name");

            return View(createMovieShowVM);

        }
        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            apiUrl = "https://localhost:7099/odata/MovieShows";
            string query = $"({id})";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);

            var movieshow = new MovieShowVM
            {
                Id = (int)id,
                Date = (DateTime)temp["Date"],
                Starttime = (DateTime)temp["Starttime"],
                Endtime = (DateTime)temp["Endtime"],
                MovieID = (int)temp["MovieID"],
                CinemaHallID = (int)temp["CinemaHallID"],
            };

            if (movieshow == null)
            {
                return NotFound();
            }
            HttpResponseMessage responses = await _httpClient.GetAsync("https://localhost:7099/odata/Movies");
            string strDatas = await responses.Content.ReadAsStringAsync();


            dynamic temps = JObject.Parse(strDatas);

            var movies = ((JArray)temps.value).Select(x => new ListMovieVM
            {
                Id = (int)x["Id"],
                Title = (string)x["Title"],
            }).ToList();
            ViewData["MovieID"] = new SelectList(movies, "Id", "Title");


            HttpResponseMessage responseMessages = await _httpClient.GetAsync("https://localhost:7099/odata/CinemaHalls");
            string stringDatas = await responseMessages.Content.ReadAsStringAsync();
            dynamic tempss = JObject.Parse(stringDatas);

            var cinemaHalls = ((JArray)tempss.value).Select(x => new ListCinemaHallVM
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();

            ViewData["CinemaHallID"] = new SelectList(cinemaHalls, "Id", "Name");
            return View(movieshow);
        }

        // POST: PropertyProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,Starttime,Endtime,MovieID,CinemaHallID")] MovieShowVM movieShowVM)
        {
            if (id != movieShowVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                apiUrl = "https://localhost:7099/odata/MovieShows";
                string query = $"({id})";
                string strData = JsonSerializer.Serialize(movieShowVM);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{apiUrl}{query}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Edit successfully!";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Message = "Error while calling WebAPI!";
                }
            }
            return View(movieShowVM);
        }
        #endregion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            apiUrl = "https://localhost:7099/odata/MovieShows";
            string query = $"({id})";

            HttpResponseMessage response = await _httpClient.GetAsync($"{apiUrl}{query}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error while calling API";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);

            var movieshow = new MovieShowVM
            {
                Id = (int)id,
                Date = (DateTime)temp["Date"],
                Starttime = (DateTime)temp["Starttime"],
                Endtime = (DateTime)temp["Endtime"],
                MovieID = (int)temp["MovieID"],
                CinemaHallID = (int)temp["CinemaHallID"],
            };

            if (movieshow == null)
            {
                return NotFound();
            }

            return View(movieshow);
        }

        // POST: PropertyProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            apiUrl = "https://localhost:7099/odata/MovieShows";
            string query = $"({id})";
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{apiUrl}{query}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Edit successfully!";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                ViewBag.Message = "Error while calling WebAPI!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
