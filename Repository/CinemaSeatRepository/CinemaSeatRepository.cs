using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
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
    public class CinemaSeatRepository : ICinemaSeatRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly CinemaSeatDAO _cinemaSeatDAO;

        public CinemaSeatRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cinemaSeatDAO = new CinemaSeatDAO(_context, _mapper);
        }
        public async Task<CreateCinemaSeatResponse> AddCinemaSeat(CreateCinemaSeatDto cinemaSeatDto)
        {
            var cinemaSeat = await _cinemaSeatDAO.CreateCinemaSeatAsync(cinemaSeatDto);
            return cinemaSeat;
        }

        public async Task<ServiceResponse<string>> DeleteCinemaSeatAsync(int id)
        {
            await _cinemaSeatDAO.DeleteCinemaSeatAsync(id);

            return new ServiceResponse<string>()
            {
                Success = true,
                Message = "Delete Success"
            };
        }

        public async Task<CinemaSeat> GetCinemaSeatByIdAsync(int id)
        {
            var cinemaSeat = await _cinemaSeatDAO.GetCinemaSeatByIdAsync(id);
            return cinemaSeat;
        }

        public async Task<ServiceResponse<List<CinemaSeat>>> GetCinemaSeats()
        {
            var cinemaSeats = await _cinemaSeatDAO.GetAllAsync();

            return new ServiceResponse<List<CinemaSeat>>()
            {
                Data = cinemaSeats,
                Success = true,
                Message = "Cinema Seat List"
            };
        }

        public async Task<CinemaSeat> UpdateCinemaSeatAsync(int id, UpdateCinemaSeatDto cinemaSeatDto)
        {
            var cinema = await _cinemaSeatDAO.UpdateCinemaSeatAsync(id, cinemaSeatDto);

            return cinema;
        }

        public async Task<List<CinemaSeatDto>> GetCinemaSeatsAsync(int cinemaHallID)
        {
            var cinemaSeats = await _cinemaSeatDAO.GetCinemaSeatsAsync(cinemaHallID);
            return cinemaSeats;
        }
    }
}
