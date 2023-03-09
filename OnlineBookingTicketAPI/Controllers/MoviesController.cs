using AutoMapper;
using BusinessObject.Entities;
using DTO.Movie;
using DTO.MovieShow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.MovieRepository;
using System.Diagnostics.CodeAnalysis;

namespace OnlineBookingTicketAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MoviesController : ODataController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [EnableQuery]
        public async Task<ActionResult> Get()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return Ok(movies);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(key);
            return movie == null ? NotFound($"Not Found MovieId {key}") : Ok(movie);
        }

        [EnableQuery]
        public async Task<IActionResult> Post(string image, [FromBody] CreateMovieDto movieDto)
        {

            await _movieRepository.AddMovie(image,movieDto);
            var model = _mapper.Map<Movie>(movieDto);

            return Created(model);
        }


        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateMovieDto movieDto)
        {
            var model = await _movieRepository.GetMovieByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await _movieRepository.UpdateMovieAsync(key, movieDto);

            return Updated(ci);
        }
    }
}
