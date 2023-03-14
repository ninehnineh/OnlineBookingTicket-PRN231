using OnlineBookingTicket.Models.CinemaSeatVMs;
using OnlineBookingTicket.Models.MovieShowVMs;

namespace OnlineBookingTicket.Models.CinemaHallVMs
{
    public class CinemaHallVM
    {
        public string Name { get; set; }
        public int Totalseats { get; set; }

        public int CinemaID { get; set; }
        public CinemaVM Cinema { get; set; }
        public List<MovieShowVM> MovieShows { get; set; }
        public List<CinemaSeatVM> CinemaSeats { get; set; }
    }
}
