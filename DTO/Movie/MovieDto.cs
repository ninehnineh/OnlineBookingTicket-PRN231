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
    }
}
