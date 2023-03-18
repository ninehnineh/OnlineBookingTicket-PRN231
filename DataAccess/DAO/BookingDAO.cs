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

        public async Task<BookingDto> GetBookingByUserIdAndMovieShowIdAsync(string userId, int movieShowId)
        {
            var booking = await _context.Bookings
                .Include(x => x.MovieShow)
                .FirstOrDefaultAsync(x => x.AppUserID == userId && x.MovieShowID == movieShowId);

            return _mapper.Map<BookingDto>(booking);
        }



        public async Task<List<BookingDto>> GetBookingsAsync(string userId)
        {
            var booking = await _context.Bookings
                .Include(x => x.MovieShow)
                .ThenInclude(x => x.Movie)
                .Include(x => x.MovieShow.CinemaHall)
                .Include(x => x.ShowSeats)
                .ThenInclude(x => x.CinemaSeat)
                .Where(x => x.AppUserID == userId)
                .ToListAsync();

            return _mapper.Map<List<BookingDto>>(booking);
        }
    }
}
