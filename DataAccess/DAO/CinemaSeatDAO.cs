using AutoMapper;
using BusinessObject.Entities;
using BusinessObject;
using DTO.CinemaHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO.CinemaSeat;

namespace DataAccess.DAO
{
    public class CinemaSeatDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;

        public CinemaSeatDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CinemaSeat>> GetAllAsync()
        {
            return await _context.CinemaSeats.ToListAsync();
        }
        public async Task<CinemaSeat> GetCinemaSeatByIdAsync(int id)
        {
            var cinema = await _context.CinemaSeats.Include(x => x.CinemaHall).SingleOrDefaultAsync(x => x.Id == id);
            return cinema;
        }

        public async Task DeleteCinemaSeatAsync(int id)
        {
            var cinema = await GetCinemaSeatByIdAsync(id);
            if (cinema != null)
            {
                _context.CinemaSeats.Remove(cinema);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<CinemaSeat> UpdateCinemaSeatAsync(int id, UpdateCinemaSeatDto cinemaSeatDto)
        {
            try
            {
                var cinemaSeat = await GetCinemaSeatByIdAsync(id);
                if (cinemaSeat == null)
                {
                    return null;
                }
                var updateCinemaSeat = _mapper.Map(cinemaSeatDto, cinemaSeat);
                _context.Update(updateCinemaSeat);
                await _context.SaveChangesAsync();

                return updateCinemaSeat;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<CreateCinemaSeatResponse> CreateCinemaSeatAsync(CreateCinemaSeatDto cinemaSeatDto)
        {
            try
            {
                CinemaSeat cinemaSeat = _mapper.Map<CinemaSeat>(cinemaSeatDto);

                await _context.CinemaSeats.AddAsync(cinemaSeat);
                await _context.SaveChangesAsync();

                return new CreateCinemaSeatResponse { Id = cinemaSeat.Id};
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CinemaSeatDetailDTO>> GetCinemaSeatsAsync(int cinemaHallID)
        {
            var cinemaSeats = await _context.CinemaSeats
                .Include(x => x.CinemaHall)
                .ThenInclude(x => x.MovieShows)
                .Where(x => x.CinemaHallID == cinemaHallID)
                .ToListAsync();

            return _mapper.Map<List<CinemaSeatDetailDTO>>(cinemaSeats);
        }
        
        public async Task<IQueryable<CinemaSeat>> GetCinemaSeatsAsyncV1(int cinemaHallID)
        {
            var cinemaSeats = _context.CinemaSeats
                .Include(x => x.CinemaHall)
                .ThenInclude(x => x.MovieShows)
                .Include(x=>x.ShowSeats)
                .Where(x => x.CinemaHallID == cinemaHallID);

            return cinemaSeats;
        }
    }
}

