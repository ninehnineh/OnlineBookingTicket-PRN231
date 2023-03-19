using BusinessObject.Entities;
using DTO.Cinema;
using DTO.CinemaHall;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CinemaHallRepository
{
    public interface ICinemaHallRepository
    {
        Task<List<CinemaHall>> GetCinemaHalls();

        Task<CinemaHall> AddCinemaHallAsync(CreateCinemaHallDto cinemaDto);

        Task<CinemaHall> GetCinemaHallByIdAsync(int id);

        Task<ServiceResponse<string>> DeleteCinemaHallAsync(int id);

        Task<CinemaHall> UpdateCinemaHallAsync(int id, UpdateCinemaHallDto cinemaDto);
    }
}
