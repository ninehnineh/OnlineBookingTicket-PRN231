using BusinessObject.Entities.Common;
using BusinessObject.Enum;
using System.Text.Json.Serialization;

namespace BusinessObject.Entities
{
    public class ShowSeat : BaseEntity
    {
        public ShowSeatStatus Status { get; set; }
        public decimal Price { get; set; }

        public int BookingID { get; set; }
        [JsonIgnore]

        public Booking Booking { get; set; }
        public int MovieShowID { get; set; }
        [JsonIgnore]
        public MovieShow MovieShow { get; set; }
        public int CinemaSeatID { get; set; }
        //[JsonIgnore]
        //public CinemaSeat CinemaSeat { get; set; }
        public CinemaSeat CinemaSeat { get; set; }

    }
}