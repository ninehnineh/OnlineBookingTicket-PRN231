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
using DataAccess.Exceptions;
using System.Globalization;

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
            var movieShows = await  _context.MovieShows.Include(a => a.Movie).Include(y => y.CinemaHall).ToListAsync();




            return movieShows;
        }

        public async Task<IQueryable<MovieShow>> GetMovieShowsAsync()
        {
            var movieShows = _context.MovieShows
                .Include(x => x.Bookings)
                .Include(z => z.ShowSeats)
                .Include(y => y.CinemaHall)
                .ThenInclude(x => x.CinemaSeats);


            return movieShows;
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

            var listMovieShow = await GetMovieShowsAsync();
            MovieShow movieShow = _mapper.Map<MovieShow>(movieShowDto);
            var movies = await _context.Movies
                .Include(x => x.MovieShows).ToListAsync();
            var movie = movies.FirstOrDefault(x => x.Id == movieShow.MovieID);
            if (movie == null)
            {
                throw new BadRequestException("Not found movie");
            }
            else
            {
                var checkDate = listMovieShow.FirstOrDefault(x => x.Date.Date == movieShowDto.Date.Date);
                var checkStartTime = listMovieShow.FirstOrDefault(x => x.Starttime == movieShowDto.Starttime);
                var checkStartTimeEqualEndTime = listMovieShow.FirstOrDefault(x => x.Endtime == movieShowDto.Starttime);
                var checkEndTime = listMovieShow.FirstOrDefault(x => x.Starttime == movieShowDto.Endtime);
                var checkEndTimeEqualStartTime = listMovieShow.FirstOrDefault(x => x.Endtime == movieShowDto.Endtime);
                int time = DateTime.Compare(movieShowDto.Endtime, movieShowDto.Starttime);
                   
                    if (checkDate != null)
                    {
                        if (checkStartTime != null || checkStartTimeEqualEndTime != null || checkEndTime != null || checkEndTimeEqualStartTime != null)
                        {
                            throw new BadRequestException("Error time");

                        }
                        else
                        {
                            foreach (var item in listMovieShow)
                            {
                                if (item.Date.Date == checkDate.Date.Date)
                                {
                                    if (movieShowDto.Starttime <= item.Endtime && movieShowDto.Starttime >= item.Starttime)
                                    {
                                        throw new BadRequestException("Error time");

                                    }
                                    else if (movieShowDto.Endtime <= item.Endtime && movieShowDto.Endtime >= item.Starttime)
                                    {
                                        throw new BadRequestException("Error time");

                                    }
                                }
                            }
                        }
                    }
                
                movieShow.Endtime = movieShow.Starttime.AddMinutes(movie.DurationInMinutes);
                await _context.MovieShows.AddAsync(movieShow);
                await _context.SaveChangesAsync();

            }

            return movieShow;
        }

    }
}

