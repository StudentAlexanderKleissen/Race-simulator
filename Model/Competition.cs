﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            if(Tracks.Any()) {
                return Tracks.Dequeue();
            } else
            {
                return null;
            }
        }
    }
}
