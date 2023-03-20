using AutoMapper;
using BusinessObject.Entities;
using DTO.Cinema;
using DTO.MovieShow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.MovieRepository;
using Repository.MovieShowRepository;
using Repository.ServiceResponse;

namespace OnlineBookingTicketAPI.Controllers
{
  
    public class MovieShowsController : ODataController
    {
        IMovieShowRepository repository;
        IMovieRepository movieRepository;
        private readonly IMapper _mapper;

        public MovieShowsController(IMovieShowRepository movieShowRepository, IMapper mapper,IMovieRepository movie)
        {
            this.repository = movieShowRepository;
            this._mapper = mapper;
            this.movieRepository = movie;
        }


        //[EnableQuery(PageSize = 1)]
        //public async Task<ActionResult<ServiceResponse<List<MovieShow>>>> Get()
        //{
        //    var list = await repository.GetMovieShows();
        //    return Ok(list);
        //}

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var movieShow = await repository.GetMovieShowByIdAsync(key);
            return movieShow == null ? NotFound($"Not Found MovieId {key}") : Ok(movieShow);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateMovieShowDto movieShowDto)
        {

            var movieShows = await repository.GetMovieShowsAsync();
            var checkDate = movieShows.FirstOrDefault(x => x.Date == movieShowDto.Date);
            var movies = await movieRepository.GetMoviesAsync();
            var movie = movies.FirstOrDefault(x => x.Id == movieShowDto.MovieID);
            if(movie == null)
            {
                return BadRequest("Movie Not Found");

            }
            else
            {
                movieShowDto.Endtime = movieShowDto.Starttime.AddMinutes(movie.DurationInMinutes);
              
                
                if (checkDate != null)
                {

                    foreach (var item in movieShows)
                    {
                        if (movieShowDto.Starttime <= item.Endtime && movieShowDto.Starttime >= item.Starttime)
                        {
                            return BadRequest("Error time");

                        }
                        else if (movieShowDto.Endtime <= item.Endtime && movieShowDto.Endtime >= item.Starttime)
                        {
                            return BadRequest("Error time");

                        }
                    }

                }
                
                await repository.AddMovieShow(movieShowDto);
                var model = _mapper.Map<MovieShow>(movieShowDto);
                return Created(model);
            }
            
            
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateMovieShowDto movieShowDto)
        {
            var model = await repository.GetMovieShowByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateMovieShowAsync(key, movieShowDto);

            return Updated(ci);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete(int key)
        {
            var p = await repository.GetMovieShowByIdAsync(key);
            if (p == null)
            {
                return NotFound();
            }
            await repository.DeleteMovieShowAsync(key);
            return Ok();
        }

        [EnableQuery]
        public async Task<ActionResult> Get()
        {
            var movieShows = await repository.GetMovieShowsAsync();
            return Ok(movieShows);
        }

        [HttpGet("movieshow")]
        [EnableQuery]

        public async Task<ActionResult> GetMovieShow()
        {
            var movieShows = await repository.GetMovieShows();
            return Ok(movieShows);
        }
    }
}
