using System;
using Controller;

namespace Race_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace);
        }
    }
}
