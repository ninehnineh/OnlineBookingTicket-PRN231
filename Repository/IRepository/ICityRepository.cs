using BusinessObject.Entities;
using DTO.Cinema;
using DTO.City;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICityRepository
    {
        Task<ServiceResponse<List<City>>> GetCities();

        Task<City> GetCityByIdAsync(int id);

        Task<City> AddCityAsync(CreateCityDto cityDto);

        Task<ServiceResponse<string>> DeleteCityAsync(int id);

        Task<City> UpdateCityAsync(int id, UpdateCityDto cityDto);
    }
}
