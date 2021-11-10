using Hangfire.Annotations;
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
    public class DataContext : INotifyPropertyChanged
    {
        public string TrackName = "Zandvoort";
        public List<IParticipant> Participants { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            //TrackName = e.Track.Name;
            Participants = e.Participants;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}