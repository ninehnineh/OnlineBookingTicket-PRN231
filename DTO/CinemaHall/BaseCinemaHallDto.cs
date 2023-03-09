using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaHall
{
    public abstract class BaseCinemaHallDto
    {
        public string Name { get; set; }
        
        public int Totalseats { get; set; }
        
        public int CinemaID { get; set; }
    }
}
