using AutoMapper;
using BusinessObject.Entities;
using DTO.Booking;
using DTO.Cinema;
using DTO.CinemaHall;
using DTO.CinemaSeat;
using DTO.City;
using DTO.Movie;
using DTO.MovieShow;
using DTO.ShowSheat;
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

            #region CinemaHall
            CreateMap<CinemaHall, CinemaHallDto>().ReverseMap();
            CreateMap<CinemaHall, CreateCinemaHallDto>().ReverseMap();
            CreateMap<CinemaHall, UpdateCinemaHallDto>().ReverseMap();
            #endregion
            
            #region CinemaSeat
            CreateMap<CinemaSeat, CinemaSeatDto>().ReverseMap();
            CreateMap<CinemaSeat, CreateCinemaSeatDto>().ReverseMap();
            CreateMap<CinemaSeat, UpdateCinemaSeatDto>().ReverseMap();
            #endregion

            #region Booking
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            #endregion

            #region Moive Show
            CreateMap<MovieShow, MovieShowDto>().ReverseMap();
            CreateMap<MovieShow, CreateMovieShowDto>().ReverseMap();
            CreateMap<MovieShow, UpdateMovieShowDto>().ReverseMap();
            #endregion

            #region Moive
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();
            CreateMap<Movie, UpdateMovieDto>().ReverseMap();
            #endregion
            
            #region Show Seat
            CreateMap<ShowSeat, ShowSeatDto>().ReverseMap();
            CreateMap<ShowSeat, CreateShowSeatDto>().ReverseMap();
            CreateMap<ShowSeat, UpdateShowSeatDto>().ReverseMap();
            #endregion

        }
    }
}
