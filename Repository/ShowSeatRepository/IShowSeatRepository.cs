using BusinessObject.Entities;
using DTO.ShowSheat;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ShowSeatRepository
{
    public interface IShowSeatRepository
    {
        Task<ServiceResponse<List<ShowSeat>>> GetAllSheatAsync();

        Task<ShowSeat> GetShowSeatByIdAsync(int id);

        Task<ShowSeat> updateshowSeatDto(UpdateShowSeatDto updateShowSeatDto);

        Task<ShowSeat> CreateShowSeatAsync(CreateShowSeatDto showSeatDto);
    }
}
