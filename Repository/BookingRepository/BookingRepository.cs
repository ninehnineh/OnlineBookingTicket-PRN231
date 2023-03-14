using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.Booking;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BookingRepository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly BookingDAO _bookingDAO;

        public BookingRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _bookingDAO = new BookingDAO(_context, _mapper);
        }
        public async Task<CreateBookingResponse> BookingAsync(CreateBookingDto bookingDto)
        {
            var bookings = await _bookingDAO.BookingAsync(bookingDto);

            return bookings;
        }
    }
}
