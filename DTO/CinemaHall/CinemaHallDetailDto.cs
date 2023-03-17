using DTO.MovieShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.CinemaHall
{
    public class CinemaHallDetailDto : BaseCinemaHallDto, IBaseDto
    {
        public int Id { get; set; }
        public List<MovieShowDetailDto> MovieShows { get; set; }
    }
}
