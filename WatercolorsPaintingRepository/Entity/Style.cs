﻿using System;
using System.Collections.Generic;

namespace WatercolorsPaintingRepository.Entity
{
    public partial class Style
    {

        public string StyleId { get; set; } = null!;
        public string StyleName { get; set; } = null!;
        public string StyleDescription { get; set; } = null!;
        public string? OriginalCountry { get; set; }
    }
}
