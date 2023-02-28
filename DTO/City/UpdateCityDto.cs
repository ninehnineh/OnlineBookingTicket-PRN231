using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.City
{
    public class UpdateCityDto : BaseCityDto, IBaseDto
    {
        public int Id { get; set; }
    }
}
