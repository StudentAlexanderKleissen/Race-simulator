﻿using System;
using System.Collections.Generic;
using System.Text;
using ExchangeAPI.Data;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Model.Competition Competition { get; set; }

        public static void Initialize()
        {
            Competition = new Model.Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Car car1 = new Car(10, 10, 10, false);
            Car car2 = new Car(10, 10, 10, false);

            Driver Alex = new Driver("Alex", 0, car1, IParticipant.TeamColors.Red);
            Driver Ander = new Driver("Ander", 0, car2, IParticipant.TeamColors.Blue);

            Competition.Participants.Add(Alex);
            Competition.Participants.Add(Ander);
        }

        public static void AddTracks()
        {
            Track track1 = new Track("Zandvoort");
            Track track2 = new Track("Monaco");

            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
        }
    }
}
