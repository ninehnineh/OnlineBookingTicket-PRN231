using BusinessObject.Entities.Common;

namespace BusinessObject.Entities
{
    public class Cinema : BaseEntity
    {
        public string Name { get; set; }
        public int TotalCinemaHalls { get; set; }

        public int CityID { get; set; }
        public City City { get; set; }
        public List<CinemaHall> CinemaHalls { get; set; }

    }
}