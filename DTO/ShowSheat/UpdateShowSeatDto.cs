using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowSheat
{
    public class UpdateShowSeatDto : BaseSheatDto,IBaseDto
    {
        public int Id { get; set; }
    }
}
