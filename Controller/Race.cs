using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random { get; set; }
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            this.Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
        }

        private SectionData GetSectionData(Section section)
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
    }
}
