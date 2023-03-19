using AutoMapper;
using BusinessObject;
using BusinessObject.Entities;
using DTO.MovieShow;
using DTO.ShowSheat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ShowSeatDAO
    {
        private OnlineBookingTicketDbContext _context;
        private readonly IMapper _mapper;
        private readonly MovieShowDAO _movieShowDAO;

        public ShowSeatDAO(OnlineBookingTicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _movieShowDAO = new MovieShowDAO(_context, _mapper);    
        }
        public async Task<List<ShowSeat>> GetAllSheatAsync()
        {
            
            return await _context.ShowSeats.ToListAsync();
        }

        public async Task<List<ShowSeat>> GetShowSeatsAsync(int movieShowID)
        {

            return await _context.ShowSeats
                .Include(x => x.CinemaSeat)
                .Where(x => x.MovieShowID == movieShowID)
                .ToListAsync();
        }

        public async Task<ShowSeat> GetShowSeatByIdAsync(int id)
        {
            var showSeat = await _context.ShowSeats.SingleOrDefaultAsync(x => x.Id == id);
            return showSeat;
        }

        public async Task<ShowSeat> CreateShowSeatAsync(CreateShowSeatDto showSeatDto)
        {
            try
            {
                ShowSeat showSeat = _mapper.Map<ShowSeat>(showSeatDto);
                showSeat.Status = BusinessObject.Enum.ShowSeatStatus.Booked;

                await _context.ShowSeats.AddAsync(showSeat);
                await _context.SaveChangesAsync();

                return showSeat;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ShowSeat> updateshowSeatDto(UpdateShowSeatDto updateShowSeatDto)
        {
            ShowSeat showSeat = await GetShowSeatByIdAsync(updateShowSeatDto.Id);
            if(showSeat == null)
            {
                return null;
            }
            else
            {
                if (showSeat.Status.Equals(BusinessObject.Enum.ShowSeatStatus.Empty))
                {
                    if(await _movieShowDAO.GetMovieShowByIdAsync(showSeat.MovieShowID) != null)
                    {
                        showSeat.Status = BusinessObject.Enum.ShowSeatStatus.Booked;
                        var updateShowSeat= _mapper.Map(updateShowSeatDto, showSeat);

                        _context.Update(updateShowSeat);
                        await _context.SaveChangesAsync();
                        return updateShowSeat;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if(showSeat.Status.Equals(BusinessObject.Enum.ShowSeatStatus.Booked))
                {
                    var movieShow = await _movieShowDAO.GetMovieShowByIdAsync(showSeat.MovieShowID);
                    if (movieShow != null)
                    {
                        if(movieShow.Endtime > DateTime.Now)
                        {
                            showSeat.Status = BusinessObject.Enum.ShowSeatStatus.Empty;
                            var updateShowSeat = _mapper.Map(updateShowSeatDto, showSeat);

                            _context.Update(showSeat);
                            await _context.SaveChangesAsync();
                            return updateShowSeat;
                        }
                        else{
                            return null;
                        }
                        
                    }
                    else
                    {
                        return null;
                    }
                }
                return showSeat;
            }    
        }
    }
}
