using DTO.CinemaHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaSeat
{
    public class CinemaSeatDetailDTO : BaseCinemaSeatDto, IBaseDto
    {
        public int Id { get; set; }
        public CinemaHallDetailDto CinemaHall { get; set; }
        public DateTime BookDate { get; set; }

    }
}
