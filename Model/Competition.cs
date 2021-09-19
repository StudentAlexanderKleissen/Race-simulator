﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }
    }
}