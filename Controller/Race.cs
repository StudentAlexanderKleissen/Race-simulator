using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Model;

namespace Controller
{
    public delegate void OnDriversChanged(object sender, DriversChangedEventArgs e);
    public class Race
    {
        public event OnDriversChanged DriversChanged;

        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random { get; set; }
        private Dictionary<Section, SectionData> _positions;
        private readonly Timer Timer;

        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            Participants = participants;
            Timer = new Timer(500);

            Timer.Elapsed += OnTimedEvents;

            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();

            SetStartPositionParticipants();
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
                int rndm1 = rnd1.Next(1, 20);
                participant.Equipment.Quality = rndm1;

                Random rnd2 = new Random();
                int rndm2 = rnd2.Next(1, 20);
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

        }

        private void Start()
        {
            Timer.Start();
        }
    }
}
