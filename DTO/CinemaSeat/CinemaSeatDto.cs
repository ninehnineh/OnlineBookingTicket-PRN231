﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CinemaSeat
{
    public class CinemaSeatDto : BaseCinemaSeatDto,IBaseDto
    {
        public int Id { get; set; }

    }
}
