using BusinessObject.Entities.Common;
using BusinessObject.Enum;
using System.Text.Json.Serialization;

namespace BusinessObject.Entities
{
    public class CinemaSeat : BaseEntity
    {
        public int SeatNumber { get; set; }
        public CinemaSeatType Type { get; set; }
        public DateTime BookDate { get; set; }

        public int CinemaHallID { get; set; }
        public CinemaHall CinemaHall { get; set; }
        //public List<ShowSeat> ShowSeats { get; set; }
        [JsonIgnore]
        public List<ShowSeat> ShowSeats { get; set; }

    }
}