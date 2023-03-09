using BusinessObject.Entities;
using DTO.Movie;
using DTO.MovieShow;
using Repository.ServiceResponse;
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
        Task<Movie> GetMovieByIdAsync(int id);

        Task<Movie> AddMovie(string image, CreateMovieDto movieDto);

        Task<ServiceResponse<string>> DeleteMovieAsync(int id);

        Task<Movie> UpdateMovieAsync(int id, UpdateMovieDto movieDto);

    }
}
