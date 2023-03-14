namespace OnlineBookingTicket.Models.CinemaSeatVMs
{
    public class CreateCinemaSeatVM
    {
        public int SeatNumber { get; set; }
        public int Type { get; set; }

        public int CinemaHallID { get; set; }
    }
}
