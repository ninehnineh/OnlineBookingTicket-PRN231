using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using BusinessObject.Enum;
using DTO.Booking;
using DTO.Cinema;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CreateBookingResponse> BookingAsync(CreateBookingDto bookingDto)
        {
            Booking booking = _mapper.Map<Booking>(bookingDto);

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return new CreateBookingResponse { Id = booking.Id } ;
        }

        public async Task<BookingDto> GetBookingAsync(string userId)
        {
            var booking = await _context.Bookings
                .Include(x => x.MovieShow)
                .FirstOrDefaultAsync(x => x.AppUserID == userId);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
