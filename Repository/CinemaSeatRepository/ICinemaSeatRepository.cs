using BusinessObject.Entities;
using DTO.Cinema;
using DTO.CinemaSeat;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CinemaSeatRepository
{
    public interface ICinemaSeatRepository
    {
        Task<ServiceResponse<List<CinemaSeat>>> GetCinemaSeats();

        Task<CreateCinemaSeatResponse> AddCinemaSeat(CreateCinemaSeatDto cinemaSeatDto);

        Task<CinemaSeat> GetCinemaSeatByIdAsync(int id);

        Task<ServiceResponse<string>> DeleteCinemaSeatAsync(int id);

        Task<CinemaSeat> UpdateCinemaSeatAsync(int id, UpdateCinemaSeatDto cinemaSeatDto);
    }
}
