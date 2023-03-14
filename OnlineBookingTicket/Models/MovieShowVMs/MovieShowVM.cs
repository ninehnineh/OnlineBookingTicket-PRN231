using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Models.CinemaHallVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Models.ShowSeatVMs;

namespace OnlineBookingTicket.Models.MovieShowVMs
{
    public class MovieShowVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public int MovieID { get; set; }
        public int CinemaHallID { get; set; }
        public CinemaHallVM CinemaHall { get; set; }
    }
}
