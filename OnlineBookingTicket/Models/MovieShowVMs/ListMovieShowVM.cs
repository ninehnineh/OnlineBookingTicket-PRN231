using BusinessObject.Entities;

namespace OnlineBookingTicket.Models.MovieShowVMs
{
    public class ListMovieShowVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public decimal Price { get; set; }
        public int MovieID { get; set; }

        public Movie Movie { get; set; }
        public int CinemaHallID { get; set; }
        public CinemaHall CinemaHall { get; set; }

    }
}
