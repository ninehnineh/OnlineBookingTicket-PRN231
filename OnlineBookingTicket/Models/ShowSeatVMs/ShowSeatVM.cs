using OnlineBookingTicket.Models.Enum;

using OnlineBookingTicket.Models.CinemaSeatVMs;

namespace OnlineBookingTicket.Models.ShowSeatVMs
{
    public class ShowSeatVM
    {
        public int Id { get; set; }
        public ShowSeatStatusVM Status { get; set; }
        public decimal Price { get; set; }

        public int BookingID { get; set; }
        public int MovieShowID { get; set; }
        public int CinemaSeatID { get; set; }

        public CinemaSeatVM CinemaSeat { get; set; }
    }
}
