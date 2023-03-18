using OnlineBookingTicket.Models.CinemaSeatVMs;

namespace OnlineBookingTicket.Models.ShowSeatVMs
{
    public class ShowSeatVM
    {
        public int CinemaSeatId { get; set; }
        public CinemaSeatVM CinemaSeat { get; set; }
    }
}
