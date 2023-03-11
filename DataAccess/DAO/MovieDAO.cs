using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DTO.City;
using DTO.Movie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MovieDAO
    {
        private readonly OnlineBookingTicketDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieDAO(OnlineBookingTicketDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public async Task<IQueryable<Movie>> GetMoviesAsync()
        {
            var movies = _dbContext.Movies.Include(x => x.MovieShows);
            return movies;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.MovieShows)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<Movie> UpdateMovieAsync(int id, UpdateMovieDto movieDto)
        {
            try
            {
                var movie = await GetMovieByIdAsync(id);
                if (movie == null)
                {
                    return null;
                }
                var updateMovie = _mapper.Map(movieDto, movie);
                _dbContext.Update(updateMovie);
                await _dbContext.SaveChangesAsync();

                return updateMovie;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<Movie> CreateMovieAsync(string image , CreateMovieDto movieDto)
        {
            try
            {
                var mybytes = System.Text.Encoding.UTF8.GetBytes(image);

                Movie movie = _mapper.Map<Movie>(movieDto);
                movie.Image = mybytes;
                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();

                return movie;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
