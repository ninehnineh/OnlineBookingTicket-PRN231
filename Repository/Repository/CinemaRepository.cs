using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DataAccess.DAO;
using DTO.Cinema;
using Repository.IRepository;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly CinemaDAO _cinemaDAO;

        public CinemaRepository(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cinemaDAO = new CinemaDAO(_context, _mapper);
        }

        public async Task<Cinema> AddCinema(CreateCinemaDto cinemaDto)
        {
            var cinema = await _cinemaDAO.CreateCinemaAsync(cinemaDto);

             return cinema;
        }

        public async Task<Cinema> GetCinemaByIdAsync(int id)
        {
            var cinema = await _cinemaDAO.GetCinemaByIdAsync(id);
            return cinema;
        }

        public async Task<ServiceResponse<List<Cinema>>> GetCinemas() { 
           var cinemas =  await _cinemaDAO.GetAllAsync();

            return new ServiceResponse<List<Cinema>>()
            {
                Data = cinemas,
                Success = true,
                Message = "Cinema List"
            };
        }
        public async Task<ServiceResponse<string>> DeleteCinemaAsync(int id)
        {
            await _cinemaDAO.DeleteCinemaAsync(id);

            return new ServiceResponse<string>()
            {
                Success = true,
                Message = "Delete Success"
            };
        }

        public async Task<Cinema> UpdateCinemaAsync(int id, UpdateCinemaDto cinemaDto)
        {
            var cinema = await _cinemaDAO.UpdateCinemaAsync(id,cinemaDto);

            return cinema;
        }
    }
}
