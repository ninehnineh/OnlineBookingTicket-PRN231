using BusinessObject.Entities.Common;

namespace BusinessObject.Entities
{
    public class CinemaHall : BaseEntity
    {
        public string Name { get; set; }
        public int Totalseats { get; set; }

        public int CinemaID { get; set; }
        public Cinema Cinema { get; set; }
        public List<MovieShow> MovieShows { get; set; }
        public List<CinemaSeat> CinemaSeats { get; set; }

    }
}