using OnlineBookingTicket.Models.BookingVMs;
using OnlineBookingTicket.Models.CinemaHallVMs;
using OnlineBookingTicket.Models.MovieVMs;
using OnlineBookingTicket.Models.ShowSeatVMs;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookingTicket.Models.MovieShowVMs
{
    public class MovieShowVM
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
              ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public decimal Price { get; set; }

        public int MovieID { get; set; }
        public MovieVM Movie { get; set; }

        public int CinemaHallID { get; set; }
        public CinemaHallVM CinemaHall { get; set; }
    }
}
