using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaSeat
{
    public class GetCinemaSeatsRequest
    {
        public int CinemaHallID { get; set; }
        public int MovieShowsId { get; set; }
        public DateTime Date { get; set; }
    }
}
