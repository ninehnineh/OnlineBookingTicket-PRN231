using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Cinema
{
    public class UpdateCinemaDto : BaseCinemaDto,IBaseDto
    {
        public int Id { get; set; }

    }
}
