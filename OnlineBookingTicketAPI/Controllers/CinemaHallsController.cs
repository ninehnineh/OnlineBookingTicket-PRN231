using AutoMapper;
using BusinessObject.Entities;
using DTO.Cinema;
using DTO.CinemaHall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.CinemaHallRepository;
using Repository.IRepository;
using Repository.ServiceResponse;

namespace OnlineBookingTicketAPI.Controllers
{
    
    public class CinemaHallsController : ODataController
    {
        ICinemaHallRepository repository;
        private readonly IMapper _mapper;

        public CinemaHallsController(ICinemaHallRepository cinemaRepository, IMapper mapper)
        {
            this.repository = cinemaRepository;
            this._mapper = mapper;
        }


        [EnableQuery(PageSize = 1)]
        public async Task<ActionResult<List<CinemaHall>>> Get()
        {
            var list = await repository.GetCinemaHalls();
            return Ok(list);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var cinema = await repository.GetCinemaHallByIdAsync(key);
            return cinema == null ? NotFound($"Not Found MovieId {key}") : Ok(cinema);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateCinemaHallDto cinema)
        {

            await repository.AddCinemaHallAsync(cinema);
            var model = _mapper.Map<CinemaHall>(cinema);

            return Created(model);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateCinemaHallDto cinema)
        {
            var model = await repository.GetCinemaHallByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateCinemaHallAsync(key, cinema);

            return Updated(ci);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete(int key)
        {
            var p = await repository.GetCinemaHallByIdAsync(key);
            if (p == null)
            {
                return NotFound();
            }
            await repository.DeleteCinemaHallAsync(key);
            return Ok();
        }


    }
}