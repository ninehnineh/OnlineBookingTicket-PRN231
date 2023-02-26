using BusinessObject.Entities.Common;

namespace BusinessObject.Entities
{
    public class Booking : BaseEntity
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }

        public int MovieShowID { get; set; }
        public MovieShow MovieShow { get; set; }
        public List<ShowSeat> ShowSeats { get; set; }
    }
}