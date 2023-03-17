using OnlineBookingTicket.Models.MovieShowVMs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineBookingTicket.Models.MovieVMs
{
    public class MovieDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int DurationInMinutes { get; set; }
        public string Language { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public List<MovieShowVM> MovieShows { get; set; }

    }
}
