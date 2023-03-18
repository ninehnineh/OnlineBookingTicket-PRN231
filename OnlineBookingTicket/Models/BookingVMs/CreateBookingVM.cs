namespace OnlineBookingTicket.Models.BookingVMs
{
    public class CreateBookingVM
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal totalPrice { get; set; }

        public int MovieShowID { get; set; }

        public string AppUserID { get; set; }
    }
}
