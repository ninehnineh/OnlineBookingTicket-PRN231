using AutoMapper;
using BusinessObject.Entities;
using BusinessObject;
using DataAccess.DAO;
using Repository.IRepository;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.City;
using DTO.Cinema;

namespace Repository.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly CityDAO _cityDAO;

        public CityRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cityDAO = new CityDAO(_context, _mapper);
        }

        public async Task<City> AddCityAsync(CreateCityDto cityDto)
        {
            var city = await _cityDAO.CreateCityAsync(cityDto);
            return city;
        }

        public async Task<ServiceResponse<string>> DeleteCityAsync(int id)
        {
            await _cityDAO.DeleteCityAsync(id);

            return new ServiceResponse<string>()
            {
                Success = true,
                Message = "Delete Success"
            };
        }

        public async Task<ServiceResponse<List<City>>> GetCities()
        {
            var cities = await _cityDAO.GetAllAsync();

            return new ServiceResponse<List<City>>()
            {
                Data = cities,
                Success = true,
                Message = "Cinema List"
            };
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            var city = await _cityDAO.GetCityByIdAsync(id);
            return city;
        }

        public async Task<City> UpdateCityAsync(int id, UpdateCityDto cityDto)
        {
            var city = await _cityDAO.UpdateCityAsync(id, cityDto);

            return city;
        }
    }
}
