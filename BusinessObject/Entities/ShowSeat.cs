using BusinessObject.Entities.Common;
using BusinessObject.Enum;

namespace BusinessObject.Entities
{
    public class ShowSeat : BaseEntity
    {
        public ShowSeatStatus Status { get; set; }
        public decimal Price { get; set; }

        public int BookingID { get; set; }
        public Booking Booking { get; set; }
        public int MovieShowID { get; set; }
        public MovieShow MovieShow { get; set; }
        public int CinemaSeatID { get; set; }
        public CinemaSeat CinemaSeat { get; set; }

    }
}