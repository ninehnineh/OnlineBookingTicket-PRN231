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

            await repository.BookingAsync(bookingDto);
            var model = _mapper.Map<Booking>(bookingDto);


            return Created(model);
        }
    }
}
