using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.Cinema;
using DTO.Movie;
using DTO.MovieShow;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MovieShowRepository
{
    public class MovieShowRepository : IMovieShowRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly MovieShowDAO _movieShowDAO;

        public MovieShowRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _movieShowDAO = new MovieShowDAO(_context, _mapper);
        }
        public async Task<MovieShow> AddMovieShow(CreateMovieShowDto movieShowDto)
        {
            var movieShow = await _movieShowDAO.CreateMovieShowAsync(movieShowDto);
            return movieShow;
        }

        public async Task<ServiceResponse<string>> DeleteMovieShowAsync(int id)
        {
            await _movieShowDAO.DeleteMovieShowAsync(id);

            return new ServiceResponse<string>()
            {
                Success = true,
                Message = "Delete Success"
            };
        }

        public async Task<MovieShow> GetMovieShowByIdAsync(int id)
        {
            var movieShow = await _movieShowDAO.GetMovieShowByIdAsync(id);
            return movieShow;
        }

        public async Task<ServiceResponse<List<MovieShow>>> GetMovieShows()
        {
            var movieShows = await _movieShowDAO.GetAllAsync();

            return new ServiceResponse<List<MovieShow>>()
            {
                Data = movieShows,
                Success = true,
                Message = "Movie Show List"
            };
        }

        public async Task<MovieShow> UpdateMovieShowAsync(int id, UpdateMovieShowDto movieShowDto)
        {
            var movieShow = await _movieShowDAO.UpdateMovieShowAsync(id, movieShowDto);

            return movieShow;
        }

        public async Task<IQueryable<MovieShow>> GetMovieShowsAsync()
        {
            return await _movieShowDAO.GetMovieShowsAsync();
        }


    }
}
