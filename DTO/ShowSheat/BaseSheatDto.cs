using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowSheat
{
    public abstract class BaseSheatDto
    {
        public decimal Price { get; set; }

        public int BookingID { get; set; } 

        public int MovieShowID { get; set; }

        public int CinemaSeatID { get; set; }

    }
}
