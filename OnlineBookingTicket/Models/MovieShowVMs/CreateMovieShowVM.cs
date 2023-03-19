using System.ComponentModel.DataAnnotations;

namespace OnlineBookingTicket.Models.MovieShowVMs
{
    public class CreateMovieShowVM
    {

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public DateTime Starttime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public DateTime Endtime { get; set; }

        public int MovieID { get; set; }
        public int CinemaHallID { get; set; }
    }
}
