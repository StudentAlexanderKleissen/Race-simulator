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
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Visualize.Initialize();

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            //Data.CurrentRace.NextRace += OnStartNextRace;
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            this.Image1.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Image1.Source = null;
                    this.Image1.Source = Visualize.DrawTrack(e.Track);
                }));
        }
    }
}
