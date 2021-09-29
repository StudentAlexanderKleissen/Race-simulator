using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Model.Competition Competition { get; set; }

        public static Race CurrentRace { get; set; }

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
            SectionTypes[] zandvoortSections =
{
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            SectionTypes[] monacoSections =
            {
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
            };

            Track track1 = new Track("Zandvoort", zandvoortSections);
            Track track2 = new Track("Monaco", monacoSections);

            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
        }

        public static Track NextRace()
        {
            Track NextRace = Competition.NextTrack();
            if (NextRace != null)
            {
                Initialize();
                CurrentRace = new Race(NextRace, Competition.Participants);
            }
            return Competition.NextTrack();
        }
    }
}
