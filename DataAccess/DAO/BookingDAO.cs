using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using BusinessObject.Enum;
using DTO.Booking;
using DTO.Cinema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookingDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ShowSeatDAO _showSeatDAO;
        public BookingDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _showSeatDAO = new ShowSeatDAO(_context, _mapper);
        }

        public async Task<Booking> BookingAsync(CreateBookingDto bookingDto)
        {
            Booking booking = _mapper.Map<Booking>(bookingDto);

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        } 
    }
}
