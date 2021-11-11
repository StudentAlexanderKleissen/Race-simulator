using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Model;

namespace Controller
{
    public static class Data
    {
        internal static object competition;

        public static Competition Competition { get; set; }

        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();

            AddTracks();
        }

        public static void AddParticipants()
        {
            Car car1 = new Car(10, 10, 5, false);
            Car car2 = new Car(10, 10, 5, false);

            Driver Alex = new Driver("Alex", 0, car1, IParticipant.TeamColors.Red);
            Driver Ander = new Driver("Xander", 0, car2, IParticipant.TeamColors.Blue);

            Competition.Participants.Add(Alex);
            Competition.Participants.Add(Ander);
        }

        public static void AddTracks()
        {
            SectionTypes[] zandvoortSections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            SectionTypes[] monacoSections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            Track zandvoort = new Track("Zandvoort", zandvoortSections);
            Track monaco = new Track("Monaco", monacoSections);
            
            //Competition.Tracks.Enqueue(monaco);
            Competition.Tracks.Enqueue(zandvoort);
            Competition.Tracks.Enqueue(monaco);
        }

        public static Track NextRace()
        {
            try
            {
                Track NextRace = Competition.NextTrack();
                if (NextRace != null)
                {
                    Initialize();
                    CurrentRace = new Race(NextRace, Competition.Participants);
                }
            }
            catch(System.NullReferenceException exception)
            {
                Console.WriteLine("System.NullReferenceException");
            }
            return Competition.NextTrack();
        }
    }
}
