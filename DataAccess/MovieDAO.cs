using BusinessObject;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MovieDAO 
    {
        private readonly OnlineBookingTicketDbContext _dbContext;

        public MovieDAO(OnlineBookingTicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Movie>> GetMoviesAsync()
        {
            var movies = _dbContext.Movies.Include(x => x.MovieShows);
            return movies;
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.MovieShows)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }
    }
}
