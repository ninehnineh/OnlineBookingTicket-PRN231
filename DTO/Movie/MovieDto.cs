using DTO.MovieShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Movie
{
    public class MovieDto : BaseMovieDto,IBaseDto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public List<MovieShowDto> MovieShows { get; set; }

    }
}
