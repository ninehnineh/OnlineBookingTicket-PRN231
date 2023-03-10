using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.CinemaHall;
using Repository.IRepository;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly CinemaHallDAO _cinemaHallDAO;

        public CinemaHallRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cinemaHallDAO = new CinemaHallDAO(_context,_mapper);
        }

        public async Task<CinemaHall> AddCinemaHall(CreateCinemaHallDto cinemaHallDto)
        {
            var cinemaHall = await _cinemaHallDAO.CreateCinemaHallAsync(cinemaHallDto);
            return cinemaHall;
        }

        public async Task<ServiceResponse<string>> DeleteCinemaHallAsync(int id)
        {
           await _cinemaHallDAO.DeleteCinemaHallAsync(id);
            return new ServiceResponse<string>()
            {
                Success = true,
                Message = "Delete Success"
            };
        }

        public async Task<CinemaHall> GetCinemaHallByIdAsync(int id)
        {
            var cinemaHall = await _cinemaHallDAO.GetCinemaHallById(id);
            return cinemaHall;
        }

        public async Task<ServiceResponse<List<CinemaHall>>> GetCinemaHalls()
        {
            var cinemaHalls = await _cinemaHallDAO.GetCinemaHallsAsync();
            return new ServiceResponse<List<CinemaHall>>()
            {
                Data = cinemaHalls,
                Message = "Cinema Hall List",
                Success = true,
            };
        }

        public async Task<CinemaHall> UpdateCinemaHallAsync(int id, UpdateCinemaHallDto cinemaHallDto)
        {
            var cinemahall = await _cinemaHallDAO.UpdateCinemaHallAsync(id, cinemaHallDto);
            return cinemahall;
        }
    }
}
