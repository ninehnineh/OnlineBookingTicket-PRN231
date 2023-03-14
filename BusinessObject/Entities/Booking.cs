using BusinessObject.Entities.Common;
using System.Text.Json.Serialization;

namespace BusinessObject.Entities
{
    public class Booking : BaseEntity
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }

        public int MovieShowID { get; set; }
        [JsonIgnore]
        public MovieShow MovieShow { get; set; }
        public string AppUserID { get; set; }
        public AppUsers AppUser { get; set; }
        public List<ShowSeat> ShowSeats { get; set; }
    }
}