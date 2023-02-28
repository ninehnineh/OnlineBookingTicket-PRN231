using BusinessObject;
using BusinessObject.Entities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly OnlineBookingTicketDbContext _dbContext;
        private readonly MovieDAO _MovieDao;

        public MovieRepository(OnlineBookingTicketDbContext dbContext)
        {
            _dbContext = dbContext;
            _MovieDao = new MovieDAO(_dbContext);
        }

        public async Task<IQueryable<Movie>> GetMoviesAsync()
        {
            return await _MovieDao.GetMoviesAsync();
        } 

        public async Task<Movie> GetMovieAsync(int id)
        {
            return await _MovieDao.GetMovieAsync(id);
        }
    }
}
