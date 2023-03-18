using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Booking
{
    public class CreateBookingDto : BaseBookingDto
    {
        public decimal totalPrice { get; set; }

    }
}
