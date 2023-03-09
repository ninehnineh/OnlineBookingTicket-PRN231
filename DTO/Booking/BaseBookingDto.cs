using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Booking
{
    public abstract class BaseBookingDto
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }

        public int MovieShowID { get; set; }

        public string AppUserID { get; set; }

    }
}
