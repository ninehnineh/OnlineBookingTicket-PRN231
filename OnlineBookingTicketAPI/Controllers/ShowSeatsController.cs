using AutoMapper;
using BusinessObject.Entities;
using DTO.MovieShow;
using DTO.ShowSheat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.ServiceResponse;
using Repository.ShowSeatRepository;

namespace OnlineBookingTicketAPI.Controllers
{

    public class ShowSeatsController : ODataController
    {
        IShowSeatRepository repository;
        private readonly IMapper _mapper;

        public ShowSeatsController(IShowSeatRepository showSeatRepository, IMapper mapper)
        {
            this.repository = showSeatRepository;
            this._mapper = mapper;
        }

        [EnableQuery]
        public async Task<ActionResult<ServiceResponse<List<ShowSeat>>>> Get()
        {
            var movies = await repository.GetAllSheatAsync();
            return Ok(movies);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var movieShow = await repository.GetShowSeatByIdAsync(key);
            return movieShow == null ? NotFound($"Not Found MovieId {key}") : Ok(movieShow);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateShowSeatDto showSeatDto)
        {

            await repository.CreateShowSeatAsync(showSeatDto);
            var model = _mapper.Map<ShowSeat>(showSeatDto);

            return Created(model);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key ,[FromBody] UpdateShowSeatDto updateShowSeatDto)
        {
            var model = await repository.GetShowSeatByIdAsync(updateShowSeatDto.Id);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.updateshowSeatDto(updateShowSeatDto);

            return Updated(ci);
        }

        [HttpGet("getShowSeatByMovieShowID/{movieShowID}")]
        [EnableQuery]
        public async Task<ActionResult> GetShowSeatByMovieShowID(int movieShowID)
        {
            var showSeats = await repository.GetShowSeatsAsync(movieShowID);
            return showSeats == null ? NotFound() : Ok(showSeats);
        }
    }
}
