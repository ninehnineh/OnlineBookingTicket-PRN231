using AutoMapper;
using BusinessObject.Entities;
using DTO.Booking;
using DTO.CinemaHall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.BookingRepository;
using Repository.CinemaHallRepository;

namespace OnlineBookingTicketAPI.Controllers
{
    
    public class BookingsController : ODataController
    {
        IBookingRepository repository;
        private readonly IMapper _mapper;

        public BookingsController(IBookingRepository bookingRepository, IMapper mapper)
        {
            this.repository = bookingRepository;
            this._mapper = mapper;
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateBookingDto bookingDto)
        {

            var booking = await repository.BookingAsync(bookingDto);

            return Ok(booking);
        }

        [HttpGet("getbooking/{userId}")]
        [EnableQuery]
        public async Task<ActionResult> Get(string userId)
        {
            var booking = await repository.GetBookingAsync(userId);
            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpGet("getbooking/{userId}/movieshow/{movieShowId}")]
        [EnableQuery]
        public async Task<ActionResult<List<BookingDto>>> GetBooking(string userId, int movieShowId)
        {
            var booking = await repository
                .GetBookingByUserIdAndMovieShowIdAsync(userId, movieShowId);
            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpGet("getbookingsbyUserId/{userId}")]
        [EnableQuery]
        public async Task<ActionResult> GetBookingsByUserID(string userId)
        {
            var booking = await repository.GetBookingsAsync(userId);
            return booking == null ? NotFound() : Ok(booking);
        }
    }
}
