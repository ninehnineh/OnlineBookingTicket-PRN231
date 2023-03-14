using OnlineBookingTicket.Models.CinemaHallVMs;
using OnlineBookingTicket.Models.ShowSeatVMs;

namespace OnlineBookingTicket.Models.CinemaSeatVMs
{
    public class CinemaSeatVM
    {
        public int SeatNumber { get; set; }
        public int Type { get; set; }

        public int CinemaHallID { get; set; }
        public CinemaHallVM CinemaHall { get; set; }
        public List<ShowSeatVM> ShowSeats { get; set; }
    }
}
