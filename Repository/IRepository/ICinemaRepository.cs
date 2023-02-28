using BusinessObject.Entities;
using DTO.Cinema;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICinemaRepository
    {
        Task<ServiceResponse<List<Cinema>>> GetCinemas();

        Task<Cinema> AddCinema(CreateCinemaDto cinemaDto);

        Task<Cinema> GetCinemaByIdAsync(int id);

        Task<ServiceResponse<string>> DeleteCinemaAsync(int id);

        Task<Cinema> UpdateCinemaAsync(int id, UpdateCinemaDto cinemaDto);
    }
}
