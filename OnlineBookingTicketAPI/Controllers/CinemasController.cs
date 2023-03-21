using AutoMapper;
using BusinessObject.Entities;
using DTO.Cinema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.ServiceResponse;

namespace OnlineBookingTicketAPI.Controllers
{
    [Authorize]

    public class CinemasController : ODataController
    {
        ICinemaRepository repository;
        private readonly IMapper _mapper;

        public CinemasController(ICinemaRepository cinemaRepository, IMapper mapper)
        {
            this.repository = cinemaRepository;
            this._mapper = mapper;
        }


        [EnableQuery(PageSize = 1)]
        public async Task<ActionResult<ServiceResponse<List<Cinema>>>> Get()
        {
            var list = await repository.GetCinemas();
            return Ok(list);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var cinema = await repository.GetCinemaByIdAsync(key);
            return cinema == null ? NotFound($"Not Found MovieId {key}") : Ok(cinema);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]CreateCinemaDto cinema)
        {

            await repository.AddCinema(cinema);
            var model = _mapper.Map<Cinema>(cinema);

            return Created(model);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateCinemaDto cinema)
        {
            var model = await repository.GetCinemaByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateCinemaAsync(key ,cinema);

            return Updated(ci);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete(int key)
        {
            var p = await repository.GetCinemaByIdAsync(key);
            if (p == null)
            {
                return NotFound();
            }
            await repository.DeleteCinemaAsync(key);
            return Ok();
        }


    }
}
