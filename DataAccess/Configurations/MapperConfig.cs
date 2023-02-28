using AutoMapper;
using BusinessObject.Entities;
using DTO.Cinema;
using DTO.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Cinema
            CreateMap<Cinema, CinemaDto>().ReverseMap();
            CreateMap<Cinema, CreateCinemaDto>().ReverseMap();
            CreateMap<Cinema, UpdateCinemaDto>().ReverseMap();
            #endregion

            #region City
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CreateCityDto>().ReverseMap();
            CreateMap<City, UpdateCityDto>().ReverseMap();
            #endregion
        }
    }
}
