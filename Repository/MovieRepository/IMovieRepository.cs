using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MovieRepository
{
    public interface IMovieRepository
    {
        Task<IQueryable<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
    }
}
