using System;
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
            //Console.SetCursorPosition(10, 0);
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

        public override string ToString()
        {
            int sectorcount = 0;
            foreach (Track track in Tracks)
            {
                sectorcount += track.Sections.Count();
            }
            return $"Participant count: {Participants.Count()}{System.Environment.NewLine}Tracks count: {Tracks.Count()}{System.Environment.NewLine}Total Sectors: {sectorcount}";
        }
        public List<string> StringToList()
        {
            int sectorcount = 0;
            List<string> list = new List<string>();
            foreach (Track track in Tracks)
            {
                sectorcount += track.Sections.Count();
            }
            foreach (Track track in Tracks)
            {
                list.Add($"Participant count: {Participants.Count()}{System.Environment.NewLine}Tracks count: {Tracks.Count() + 1}{System.Environment.NewLine}Total Sectors: {sectorcount}");
            }
            //list.Add("");
            return list;
        }

        public List<string> ListToTrackList()
        {
            List<string> list = new List<string>();
            //foreach(Track track in Tracks)
            //{
            //    list.Add($"Track name: {track.Name}{System.Environment.NewLine}Sections: {track.AmountOfCornerSections + track.AmountOfStraightSections}{System.Environment.NewLine}Straight Sections: {track.AmountOfStraightSections}{System.Environment.NewLine}Corner Sections: {track.AmountOfCornerSections}");
            //}
            list.Add($"Track name: Zandvoort{System.Environment.NewLine}Sectors: 20{System.Environment.NewLine}Straight sectors: 14{System.Environment.NewLine}Corners: 4");
            list.Add($"Track name: Monaco{System.Environment.NewLine}Sectors: 24{System.Environment.NewLine}Straight sectors: 20{System.Environment.NewLine}Corners: 4");
            return list;
        }

        public List<string> DriverStandingsList()
        {
            List<string> list = new List<string>();
            foreach (IParticipant participant in Participants)
            {
                string DriverStandings = $"Name: {participant.Name}{System.Environment.NewLine}Team: {participant.TeamColor}{System.Environment.NewLine}Points: {participant.Points}";
                list.Add(DriverStandings);
            }
            return list;
        }
    }
}
