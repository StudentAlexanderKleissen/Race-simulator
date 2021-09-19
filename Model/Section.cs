﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }
    class Section
    {
        public SectionTypes SectionType { get; set; }
    }
}
