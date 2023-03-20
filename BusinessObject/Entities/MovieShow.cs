using BusinessObject.Entities.Common;
using System.Text.Json.Serialization;

namespace BusinessObject.Entities
{
    public class MovieShow : BaseEntity
    {
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public decimal Price { get; set; }


        public int MovieID { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }
        public int CinemaHallID { get; set; }
        [JsonIgnore]
        public CinemaHall CinemaHall { get; set; }

        public List<Booking> Bookings { get; set; }
        public List<ShowSeat> ShowSeats { get; set; }
    }
}