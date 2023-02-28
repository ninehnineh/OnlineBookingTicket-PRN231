using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DTO.Cinema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CinemaDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;

        public CinemaDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _context.Cinemas.ToListAsync();
        }
        public async Task<Cinema> GetCinemaByIdAsync(int id)
        {
            var cinema = await _context.Cinemas.Include(x => x.City).SingleOrDefaultAsync(x => x.Id == id);
            return cinema; 
        }
        
        public async Task DeleteCinemaAsync(int id)
        {
            var cinema = await GetCinemaByIdAsync(id);
            if(cinema != null)
            {
                _context.Cinemas.Remove(cinema);
                await _context.SaveChangesAsync();  
            }
        }
        public async Task<Cinema> UpdateCinemaAsync(int id ,UpdateCinemaDto cinemaDto)
        {
            try
            {
                var cinema = await GetCinemaByIdAsync(id);
                if (cinema == null)
                {
                    return null;
                }
                var updateCinema = _mapper.Map(cinemaDto, cinema);
                _context.Update(updateCinema);                
                await _context.SaveChangesAsync();

                return updateCinema;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<Cinema> CreateCinemaAsync(CreateCinemaDto cinemaDto)
        {
            try
            {
                Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

                await _context.Cinemas.AddAsync(cinema);
                await _context.SaveChangesAsync();

                return cinema;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
