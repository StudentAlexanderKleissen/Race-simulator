using System;
using System.Threading;
using Controller;
using Model;

namespace Race_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            Visualize.Initialize();
            Visualize.DrawTrack(Data.CurrentRace.track);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
