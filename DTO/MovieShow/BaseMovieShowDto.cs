using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MovieShow
{
    public abstract class BaseMovieShowDto
    {
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }


        public int MovieID { get; set; }
        public int CinemaHallID { get; set; }

    }
}
