﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class AppUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
