namespace OnlineBookingTicket.Models.MovieVMs
{
    public class MovieDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int DurationInMinutes { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }

        //public List<MovieShow> MovieShows { get; set; }

    }
}
