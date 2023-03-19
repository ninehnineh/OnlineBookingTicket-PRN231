using BusinessObject.Entities;
using DTO.Cinema;
using DTO.MovieShow;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MovieShowRepository
{
    public interface IMovieShowRepository
    {
        Task<List<MovieShow>> GetMovieShows();

        Task<MovieShow> AddMovieShow(CreateMovieShowDto movieShowDto);

        Task<MovieShow> GetMovieShowByIdAsync(int id);

        Task<ServiceResponse<string>> DeleteMovieShowAsync(int id);

        Task<MovieShow> UpdateMovieShowAsync(int id, UpdateMovieShowDto movieShowDto);

        Task<IQueryable<MovieShow>> GetMovieShowsAsync();
    }
}
