﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaSeat
{
    public class CreateCinemaSeatDto : BaseCinemaSeatDto
    {
        public DateTime BookDate { get; set; }
    }
}
