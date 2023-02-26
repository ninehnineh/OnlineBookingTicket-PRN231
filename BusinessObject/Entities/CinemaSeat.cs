using BusinessObject.Entities.Common;
using BusinessObject.Enum;

namespace BusinessObject.Entities
{
    public class CinemaSeat : BaseEntity
    {
        public int SeatNumber { get; set; }
        public CinemaSeatType Type { get; set; }

        public int CinemaHallID { get; set; }
        public CinemaHall CinemaHall { get; set; }
        public List<ShowSeat> ShowSeats { get; set; }

    }
}