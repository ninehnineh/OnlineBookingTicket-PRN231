using AutoMapper;
using BusinessObject.Entities;
using DTO.CinemaHall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.ServiceResponse;


namespace OnlineBookingTicketAPI.Controllers
{
    public class CinemaHallsController : ODataController
    {
        ICinemaHallRepository repository;
        private readonly IMapper _mapper;

        public CinemaHallsController(ICinemaHallRepository cinemaHallRepository, IMapper mapper)
        {
            this.repository = cinemaHallRepository;
            this._mapper = mapper;
        }


        [EnableQuery(PageSize = 1)]
        public async Task<ActionResult<ServiceResponse<List<CinemaHall>>>> Get()
        {
            var list = await repository.GetCinemaHalls();
            return Ok(list);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var cinemaHall = await repository.GetCinemaHallByIdAsync(key);
            return cinemaHall == null ? NotFound($"Not Found CinemaHallId {key}") : Ok(cinemaHall);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateCinemaHallDto cinemaHall)
        {

            await repository.AddCinemaHall(cinemaHall);
            var model = _mapper.Map<CinemaHall>(cinemaHall);

            return Created(model);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateCinemaHallDto cinemaHall)
        {
            var model = await repository.GetCinemaHallByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateCinemaHallAsync(key, cinemaHall);

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
