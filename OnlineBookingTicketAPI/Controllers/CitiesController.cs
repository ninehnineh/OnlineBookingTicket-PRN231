using AutoMapper;
using BusinessObject.Entities;
using DTO.Cinema;
using DTO.City;
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

    public class CitiesController : ODataController
    {
        ICityRepository repository;
        private readonly IMapper _mapper;

        public CitiesController(ICityRepository cinemaRepository, IMapper mapper)
        {
            this.repository = cinemaRepository;
            this._mapper = mapper;
        }


        [EnableQuery(PageSize = 1)]
        public async Task<ActionResult<List<City>>> Get()
        {
            var list = await repository.GetCities();
            return Ok(list);
        }

        [EnableQuery]
        public async Task<ActionResult> Get(int key)
        {
            var city = await repository.GetCityByIdAsync(key);
            return city == null ? NotFound($"Not Found MovieId {key}") : Ok(city);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateCityDto cityDto)
        {

            await repository.AddCityAsync(cityDto);
            var model = _mapper.Map<City>(cityDto);

            return Created(model);
        }

        [EnableQuery]
        public async Task<IActionResult> Put(int key, [FromBody] UpdateCityDto cityDto)
        {
            var model = await repository.GetCityByIdAsync(key);
            if (model == null)
            {
                return NotFound();
            }
            var ci = await repository.UpdateCityAsync(key, cityDto);

            return Updated(ci);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete(int key)
        {
            var p = await repository.GetCityByIdAsync(key);
            if (p == null)
            {
                return NotFound();
            }
            await repository.DeleteCityAsync(key);
            return Ok();
        }
    }
}
