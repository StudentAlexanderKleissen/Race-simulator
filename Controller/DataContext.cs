
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hangfire.Annotations;

namespace Controller
{
    public class DataContext : INotifyPropertyChanged
    {
        public string TrackName => Data.CurrentRace.track.Name;
        public List<IParticipant> Participants { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DataContext()
        {
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}