using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DTO.City;
using DTO.Movie;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.DAO
{
    public class MovieDAO
    {
        private readonly OnlineBookingTicketDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;

        public MovieDAO(OnlineBookingTicketDbContext dbContext,
            IMapper mapper,
            IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .Include(x => x.MovieShows).ToListAsync();
                
            return movies;
        }
        
        public async Task<IEnumerable<MovieDto>> GetMoviesAsyncV1()
        {
            var movies = await _dbContext.Movies
                .Include(x => x.MovieShows)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.MovieShows)
                .ThenInclude(x => x.CinemaHall)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<Movie> UpdateMovieAsync(int id, UpdateMovieDto movieDto)
        {
            //try
            //{
            //    var movie = await GetMovieByIdAsync(id);
            //    if (movie == null)
            //    {
            //        return null;
            //    }
            //    var updateMovie = _mapper.Map(movieDto, movie);
            //    _dbContext.Update(updateMovie);
            //    await _dbContext.SaveChangesAsync();

            //    return updateMovie;

            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}
            return null;
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
        
        public async Task<Movie> CreateMovieAsyncV1(IFormFile file, CreateMovieDto movieDto)
        {
            try
            {
                Movie movie = _mapper.Map<Movie>(movieDto);
                if (file != null && file.Length > 0)
                {
                    await UploadImage(file);
                    movie.Image = System.Text.Encoding.UTF8.GetBytes(file.FileName);
                }

                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();

                return movie;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UploadImage(IFormFile file)
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, @"..\OnlineBookingTicket\wwwroot\Image", file.FileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
