using BusinessObject.Entities;
using DTO.CinemaHall;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICinemaHallRepository
    {
        Task<ServiceResponse<List<CinemaHall>>> GetCinemaHalls();

        Task<CinemaHall> AddCinemaHall(CreateCinemaHallDto cinemaHallDto);

        Task<CinemaHall> GetCinemaHallByIdAsync(int id);

        Task<ServiceResponse<string>> DeleteCinemaHallAsync(int id);

        Task<CinemaHall> UpdateCinemaHallAsync(int id, UpdateCinemaHallDto updateCinemaHallDto);
    }
}
