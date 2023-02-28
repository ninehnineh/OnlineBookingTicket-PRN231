using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Cinema
{
    public abstract class BaseCinemaDto
    {
        public string Name { get; set; }

        public int TotalCinemaHalls { get; set; }

        public int CityID { get; set; }
    }
}
