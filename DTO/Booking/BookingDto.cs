using BusinessObject.Entities;
using DTO.MovieShow;
using DTO.ShowSheat;
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
        public decimal TotalPrice { get; set; }


        public int MovieShowID { get; set; }
        public ListMovieShowDto MovieShow { get; set; }

        public string AppUserID { get; set; }
        public List<ShowSeatDto> ShowSeats { get; set; }


    }
}
