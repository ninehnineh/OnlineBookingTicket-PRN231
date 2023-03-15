using AutoMapper;
using BusinessObject.Entities;
using DTO.CinemaSeat;
using DTO.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.CinemaSeatRepository;
using Repository.IRepository;
using Repository.ServiceResponse;

namespace OnlineBookingTicketAPI.Controllers
{
   
    public class CinemaSeatsController : ODataController
    {
        ICinemaSeatRepository repository;
        private readonly IMapper _mapper;

        public CinemaSeatsController(ICinemaSeatRepository cinemaSeatRepository, IMapper mapper)
        {
            this.repository = cinemaSeatRepository;
            this._mapper = mapper;
        }

        [HttpGet("getall")]
        [EnableQuery(PageSize = 1)]
        public async Task<ActionResult<ServiceResponse<List<CinemaSeat>>>> Get()
        {
            var list = await repository.GetCinemaSeats();
            return Ok(list);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var cinemaSeat = await repository.GetCinemaSeatByIdAsync(key);
            return cinemaSeat == null ? NotFound($"Not Found MovieId {key}") : Ok(cinemaSeat);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateCinemaSeatDto cinemaSeatDto)
        {

            var cinemaSeat = await repository.AddCinemaSeat(cinemaSeatDto);

            return Ok(cinemaSeat);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateCinemaSeatDto cinemaSeatDto)
        {
            var model = await repository.GetCinemaSeatByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateCinemaSeatAsync(key, cinemaSeatDto);

            return Updated(ci);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete(int key)
        {
            var p = await repository.GetCinemaSeatByIdAsync(key);
            if (p == null)
            {
                return NotFound();
            }
            await repository.DeleteCinemaSeatAsync(key);
            return Ok();
        }

        [HttpGet("getcinemaseats/{cinemaHallID}")]
        [EnableQuery]
        public async Task<ActionResult> GetCinemaSeatsByHallID(int cinemaHallID)
        {
            var cinemaSeats = await repository.GetCinemaSeatsAsync(cinemaHallID);
            return cinemaSeats.Count == 0  ? NoContent() : Ok(cinemaSeats);
        }
    }
}
