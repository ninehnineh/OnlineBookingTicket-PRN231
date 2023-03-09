using AutoMapper;
using BusinessObject.Entities;
using BusinessObject;
using DTO.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO.MovieShow;

namespace DataAccess.DAO
{
    public class MovieShowDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;

        public MovieShowDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MovieShow>> GetAllAsync()
        {
            return await _context.MovieShows.ToListAsync();
        }
        public async Task<MovieShow> GetMovieShowByIdAsync(int id)
        {
            var movieShow = await _context.MovieShows
                .Include(x => x.ShowSeats)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movieShow;
        }



        public async Task DeleteMovieShowAsync(int id)
        {
            var movieShow = await GetMovieShowByIdAsync(id);
            if (movieShow != null)
            {
                _context.MovieShows.Remove(movieShow);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<MovieShow> UpdateMovieShowAsync(int id, UpdateMovieShowDto moviceShowDto)
        {
            try
            {
                var movieShow = await GetMovieShowByIdAsync(id);
                if (movieShow == null)
                {
                    return null;
                }
                var updateMovieShow = _mapper.Map(moviceShowDto, movieShow);
                _context.Update(updateMovieShow);
                await _context.SaveChangesAsync();

                return updateMovieShow;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<MovieShow> CreateMovieShowAsync(CreateMovieShowDto movieShowDto)
        {
            try
            {
                MovieShow movieShow = _mapper.Map<MovieShow>(movieShowDto);

                await _context.MovieShows.AddAsync(movieShow);
                await _context.SaveChangesAsync();

                return movieShow;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

