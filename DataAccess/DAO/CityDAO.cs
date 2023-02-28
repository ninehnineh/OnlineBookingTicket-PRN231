using AutoMapper;
using BusinessObject.Entities;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO.Cinema;
using DTO.City;

namespace DataAccess.DAO
{
    public class CityDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;

        public CityDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }
        public async Task<City> GetCityByIdAsync(int id)
        {
            var cinema = await _context.Cities
                .Include(x => x.Cinemas)
                .FirstOrDefaultAsync(x => x.Id == id);

            return cinema;
        }

       

        public async Task DeleteCityAsync(int id)
        {
            var cinema = await GetCityByIdAsync(id);
            if (cinema != null)
            {
                _context.Cities.Remove(cinema);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<City> UpdateCityAsync(int id, UpdateCityDto cityDto)
        {
            try
            {
                var city = await GetCityByIdAsync(id);
                if (city == null)
                {
                    return null;
                }
                var updateCity = _mapper.Map(cityDto, city);
                _context.Update(updateCity);
                await _context.SaveChangesAsync();

                return updateCity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<City> CreateCityAsync(CreateCityDto cityDto)
        {
            try
            {
                City city = _mapper.Map<City>(cityDto);

                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();

                return city;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
