using BusinessObject.Entities.Common;

namespace BusinessObject.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public List<Cinema> Cinemas { get; set; }

    }
}