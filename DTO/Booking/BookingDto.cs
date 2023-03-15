using DTO.MovieShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Booking
{
    public class BookingDto
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int MovieShowID { get; set; }
        public string AppUserID { get; set; }
        public MovieShowDto MovieShow { get; set; }

    }
}
