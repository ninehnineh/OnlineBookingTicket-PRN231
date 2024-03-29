﻿using OnlineBookingTicket.Models.Enum;

namespace OnlineBookingTicket.Models.ShowSeatVMs
{
    public class CreateShowSeatVM
    {
        public ShowSeatStatusVM Status { get; set; }
        public decimal Price { get; set; }

        public int BookingID { get; set; }
        public int MovieShowID { get; set; }
        public int CinemaSeatID { get; set; }
    }
}
