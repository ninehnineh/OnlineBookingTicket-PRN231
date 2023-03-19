using OnlineBookingTicket.Models.MovieShowVMs;
using OnlineBookingTicket.Models.ShowSeatVMs;

namespace OnlineBookingTicket.Models.BookingVMs
{
    public class BookingVM
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal TotalPrice { get; set; }


        public int MovieShowID { get; set; }
        public MovieShowVM MovieShow { get; set; }
        public string AppUserID { get; set; }
        public List<ShowSeatVM> ShowSeats { get; set; }

    }
}
