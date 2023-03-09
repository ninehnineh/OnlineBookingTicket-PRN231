using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MovieShow
{
    public class UpdateMovieShowDto : BaseMovieShowDto,IBaseDto
    {
        public int Id { get; set; }
    }
}
