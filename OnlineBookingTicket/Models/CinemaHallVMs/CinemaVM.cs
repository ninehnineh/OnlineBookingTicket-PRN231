using OnlineBookingTicket.Models.CityVMs;

namespace OnlineBookingTicket.Models.CinemaHallVMs
{
    public class CinemaVM
    {
        public string Name { get; set; }
        public int TotalCinemaHalls { get; set; }

        public int CityID { get; set; }
        public CityVM City { get; set; }
        public List<CinemaHallVM> CinemaHalls { get; set; }
    }
}