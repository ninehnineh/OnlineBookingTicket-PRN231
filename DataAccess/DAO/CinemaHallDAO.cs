using AutoMapper;
using BusinessObject.Entities;
using BusinessObject;
using DTO.Cinema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO.CinemaHall;

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

        public async Task<List<CinemaHall>> GetAllAsync()
        {
            return await _context.CinemaHalls.ToListAsync();
        }
        public async Task<CinemaHall> GetCinemaHallByIdAsync(int id)
        {
            var cinema = await _context.CinemaHalls.Include(x => x.CinemaSeats).SingleOrDefaultAsync(x => x.Id == id);
            return cinema;
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            var cinema = await GetCinemaHallByIdAsync(id);
            if (cinema != null)
            {
                _context.CinemaHalls.Remove(cinema);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<CinemaHall> UpdateCinemaHallAsync(int id, UpdateCinemaHallDto cinemaHallDto)
        {
            try
            {
                var cinemaHall = await GetCinemaHallByIdAsync(id);
                if (cinemaHall == null)
                {
                    return null;
                }
                var updateCinemaHall = _mapper.Map(cinemaHallDto, cinemaHall);
                _context.Update(updateCinemaHall);
                await _context.SaveChangesAsync();

                return updateCinemaHall;

            }
            catch (Exception e)
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

