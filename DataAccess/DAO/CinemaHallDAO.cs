using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DTO.Cinema;
using DTO.CinemaHall;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CinemaHallDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;

        public CinemaHallDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CinemaHall>> GetCinemaHallsAsync()
        {
            return await _context.CinemaHalls.ToListAsync();
        }

        public async Task<CinemaHall> GetCinemaHallById(int id)
        {
            var cinemaHall = await _context.CinemaHalls.Include(x => x.Cinema).SingleOrDefaultAsync(x => x.Id == id);
            return cinemaHall;
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            var cinemaHall = await GetCinemaHallById(id);
            if (cinemaHall != null)
            {
                _context.CinemaHalls.Remove(cinemaHall);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CinemaHall> UpdateCinemaHallAsync(int id, UpdateCinemaHallDto cinemaHallDto)
        {
            try
            {
                var cinemaHall = await GetCinemaHallById(id);
                if (cinemaHall == null)
                {
                    return null;
                }
                var updatedCinemaHall = _mapper.Map(cinemaHallDto, cinemaHall);
                _context.Update(updatedCinemaHall);
                await _context.SaveChangesAsync();
                return updatedCinemaHall;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<CinemaHall> CreateCinemaHallAsync(CreateCinemaHallDto cinemaHallDto)
        {
            try
            {
                CinemaHall cinemaHall = _mapper.Map<CinemaHall>(cinemaHallDto);
                await _context.CinemaHalls.AddAsync(cinemaHall);
                await _context.SaveChangesAsync();
                return cinemaHall;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
