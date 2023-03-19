using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.ShowSheat;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ShowSeatRepository
{
    public class ShowSeatRepository : IShowSeatRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ShowSeatDAO _showSeatDAO;

        public ShowSeatRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _showSeatDAO = new ShowSeatDAO(_context, _mapper);
        }

        public async Task<ShowSeat> CreateShowSeatAsync(CreateShowSeatDto showSeatDto)
        {
            return await _showSeatDAO.CreateShowSeatAsync(showSeatDto); 
        }

        public async Task<ServiceResponse<List<ShowSeat>>> GetAllSheatAsync()
        {
            var showSeat =  await _showSeatDAO.GetAllSheatAsync();
            return new ServiceResponse<List<ShowSeat>>()
            {
                Data = showSeat,
                Success = true,
                Message = "List Show Seat",
            };
        }

        public async Task<ShowSeat> GetShowSeatByIdAsync(int id)
        {
            var showSeat = await _showSeatDAO.GetShowSeatByIdAsync(id);
            return showSeat;
        }

        public async Task<ShowSeat> updateshowSeatDto(UpdateShowSeatDto updateShowSeatDto)
        {
            var showSeat = await _showSeatDAO.updateshowSeatDto(updateShowSeatDto);
            return showSeat;
        }

        public async Task<List<ShowSeat>> GetShowSeatsAsync(int movieShowID)
        {
            var showseats = await _showSeatDAO.GetShowSeatsAsync(movieShowID);
            return showseats;
        }

    }
}
