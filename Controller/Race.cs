using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Model;

namespace Controller
{
    public delegate void OnDriversChanged(object sender, DriversChangedEventArgs e);
    public delegate void OnNextRace(object sender, DriversChangedEventArgs e);
    public class Race
    {
        public event OnDriversChanged DriversChanged;
        public event OnNextRace NextRace;

        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Dictionary<Section, SectionData> _positions;
        private readonly Timer Timer;

        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            Participants = participants;
            Timer = new Timer(100);

            Timer.Elapsed += OnTimedEvents;

            _positions = new Dictionary<Section, SectionData>();

            RandomizeEquiment();
            SetStartPositionParticipants();
            Start();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }

        public void RandomizeEquiment()
        {
            foreach(IParticipant participant in Participants)
            {
                Random rnd1 = new Random();
                int rndm1 = rnd1.Next(1, 5);
                participant.Equipment.Quality = rndm1;

                Random rnd2 = new Random();
                int rndm2 = rnd2.Next(1, 5);
                participant.Equipment.Performance = rndm2;
            }
        }

        public override string ToString()
        {
            return track.Name;
        }

        private void SetStartPositionParticipants()
        {
            foreach (Section section in track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                if (section.SectionType == SectionTypes.StartGrid)
                {
                    sectionData.Left = Participants[0];
                    sectionData.DistanceLeft = 0;
                    sectionData.Right = Participants[1];
                    sectionData.DistanceRight = 0;
                }
            }
        }

        private void OnTimedEvents(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();
            //Console.WriteLine("Dit is een test");
            DriversChangedEventArgs driversChangedEventArgs = new DriversChangedEventArgs(track, Participants);
            DriversChanged?.Invoke(this, driversChangedEventArgs);
            if (driversChangedEventArgs.EveryoneHasFinished == true)
            {
                driversChangedEventArgs.EveryoneHasFinished = false;
                StartNextRace(driversChangedEventArgs);
            }
            Timer.Start();
        }

        private void Start()
        {
            Timer.Start();
        }

        private void StartNextRace(DriversChangedEventArgs driversChangedEventArgs)
        {
            Timer.Stop();

            Timer.Elapsed -= OnTimedEvents;

            NextRace?.Invoke(this, driversChangedEventArgs);
        }
    }
}
