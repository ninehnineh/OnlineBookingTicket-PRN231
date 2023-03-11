using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaSeat
{
    public abstract class BaseCinemaSeatDto
    {
        public int SeatNumber { get; set; }
        public CinemaSeatType Type { get; set; }

        public int CinemaHallID { get; set; }


    }
}
