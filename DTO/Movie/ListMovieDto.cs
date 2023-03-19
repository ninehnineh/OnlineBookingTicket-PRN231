using DTO.MovieShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.Movie
{
    public class ListMovieDto : BaseMovieDto, IBaseDto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        [JsonIgnore]
        public List<ListMovieShowDto> MovieShows { get; set; }
    }
}
