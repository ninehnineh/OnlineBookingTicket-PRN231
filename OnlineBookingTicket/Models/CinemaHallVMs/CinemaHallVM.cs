using OnlineBookingTicket.Models.CinemaSeatVMs;
using OnlineBookingTicket.Models.MovieShowVMs;
using System.Text.Json.Serialization;

namespace OnlineBookingTicket.Models.CinemaHallVMs
{
    public class CinemaHallVM
    {
        public string Name { get; set; }
        public int Totalseats { get; set; }

        public int CinemaID { get; set; }

        [JsonIgnore]
        public CinemaVM Cinema { get; set; }
        [JsonIgnore]
        public List<MovieShowVM> MovieShows { get; set; }
        [JsonIgnore]
        public List<CinemaSeatVM> CinemaSeats { get; set; }
    }
}
