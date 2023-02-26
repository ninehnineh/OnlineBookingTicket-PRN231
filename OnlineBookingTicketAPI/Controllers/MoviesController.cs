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

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
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
            var movie = await _movieRepository.GetMovieAsync(key);
            return movie == null ? NotFound($"Not Found MovieId {key}") : Ok(movie);
        }
    }
}
