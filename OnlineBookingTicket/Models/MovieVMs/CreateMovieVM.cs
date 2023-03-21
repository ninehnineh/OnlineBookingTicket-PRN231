using System.ComponentModel.DataAnnotations;

namespace OnlineBookingTicket.Models.MovieVMs
{
    public class CreateMovieVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int DurationInMinutes { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Genre { get; set; }
        //[Required]
        //public IFormFile Image { get; set; }
    }
}
