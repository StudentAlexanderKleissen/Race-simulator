
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hangfire.Annotations;
using System.Linq;
using System;

namespace Controller
{
    public class DataContext : INotifyPropertyChanged
    {
        public string TrackName => Data.CurrentRace.track.Name;
        public int CurrentTrackCorners => Data.CurrentRace.track.AmountOfCornerSections;
        public int CurrentTrackStraight => Data.CurrentRace.track.AmountOfStraightSections;
        public Track CurrentTrack => Data.CurrentRace.track;
        public IEnumerable<IParticipant> Participants => Data.Competition.Participants.Select(x => x);
        public IEnumerable<String> ParticipantsName => Data.Competition.Participants.Select(x => x.Name);
        public IEnumerable<string> ParticipantsTeams => Data.Competition.Participants.Select(x => x.TeamColor.ToString());
        public IEnumerable<int> ParticipantsPoints => Data.Competition.Participants.Select(x => x.Points);
        public IEnumerable<string> EntireCompetition => Data.Competition.StringToList().Select(x => x);
        public IEnumerable<string> EveryTrack => Data.Competition.ListToTrackList();
        public IEnumerable<string> DriverStandings => Data.Competition.DriverStandingsList().Select(x => x);

        public event PropertyChangedEventHandler PropertyChanged;
        
        public DataContext()
        {
            Data.Initialize();
            Data.NextRace();
            Data.Competition.Participants[0].Points = Data.Competition.Participants[0].TimesWon * 18;
            Data.Competition.Participants[1].Points = Data.Competition.Participants[1].TimesWon * 18;

            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Data.Competition.Participants[0].Points = Data.Competition.Participants[0].TimesWon * 18;
            Data.Competition.Participants[1].Points = Data.Competition.Participants[1].TimesWon * 18;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}