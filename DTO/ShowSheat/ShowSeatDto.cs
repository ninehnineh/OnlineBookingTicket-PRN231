﻿using DTO.CinemaSeat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShowSheat
{
    public class ShowSeatDto
    {
        public int CinemaSeatId { get; set; }
        public CinemaSeatDto CinemaSeat { get; set; }
    }
}
