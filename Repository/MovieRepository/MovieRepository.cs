using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.Movie;
using DTO.MovieShow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repository.ServiceResponse;
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
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;

        public MovieRepository(OnlineBookingTicketDbContext dbContext,
            IMapper mapper,
            IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _environment = environment;
            _MovieDao = new MovieDAO(_dbContext,_mapper, _environment);
        }

        public async Task<IQueryable<Movie>> GetMoviesAsync()
        {
            return await _MovieDao.GetMoviesAsync();
        } 

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _MovieDao.GetMovieByIdAsync(id);
        }

        public async Task<Movie> AddMovie(string image, CreateMovieDto movieDto)
        {
            var movie = await _MovieDao.CreateMovieAsync(image, movieDto);
            return movie;
        }
        
        public async Task<Movie> AddMovie(IFormFile image, CreateMovieDto movieDto)
        {
            var movie = await _MovieDao.CreateMovieAsyncV1(image, movieDto);
            return movie;
        }

        public Task<ServiceResponse<string>> DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> UpdateMovieAsync(int id, UpdateMovieDto movieDto)
        {
            var movie = await _MovieDao.UpdateMovieAsync(id, movieDto);

            return movie;
        }
    }
}
