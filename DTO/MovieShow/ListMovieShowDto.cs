using DTO.CinemaHall;
using DTO.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.MovieShow
{
    public class ListMovieShowDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }


        public int MovieID { get; set; }
        public int CinemaHallID { get; set; }
        public CinemaHallDto CinemaHall { get; set; }

        public ListMovieDto Movie { get; set; }
    }
}
