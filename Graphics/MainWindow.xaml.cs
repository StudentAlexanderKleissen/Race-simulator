using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IsZandvoortFinished;
        public static bool IsMonacoFinished;
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Visualize.Initialize();

            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            if(IsZandvoortFinished == true && IsMonacoFinished == true)
            {
                Data.CurrentRace.DriversChanged -= OnDriversChanged;
                UseImages.Clear();
            }

            this.Label1.Dispatcher.BeginInvoke(
        DispatcherPriority.Render,
            new Action(() =>
            {
                this.Label1.Content = null;
                this.Label1.Content = $"Zandvoort: {IsZandvoortFinished}, Monaco: {IsMonacoFinished}";
            }));

            Visualize.Player1 = e.Participants[0];
            Visualize.Player2 = e.Participants[1];

            this.Image1.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Image1.Source = null;
                    this.Image1.Source = Visualize.DrawTrack(e.Track);
                }));


            if (Visualize.Loser == Visualize.Player1 || Visualize.Loser == Visualize.Player2)
            {
                e.EveryoneHasFinished = true;

                if(e.Track.Name == "Zandvoort")
                {
                    IsZandvoortFinished = true;
                } else if(e.Track.Name == "Monaco")
                {
                    IsMonacoFinished = true;
                }

                this.Label1.Dispatcher.BeginInvoke(
                    DispatcherPriority.Render,
                    new Action(() =>
                    {
                        this.Label1.Content = null;
                        this.Label1.Content = "Race beindigd";
                    }));
                UseImages.Clear();
                Visualize.Initialize();
                Visualize.bitmap = UseImages.GetEmptyBitmap(800, 500);
            }
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
