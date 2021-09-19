using System;
using System.Collections.Generic;
using System.Text;
using Betfair_API_NG.TO;
using Model;
using static Model.IParticipant;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }

        public static void Initialize()
        {
            //Competition = new Competition();
        }

        public static void AddParticipant()
        {
            Car car1 = new Car(10, 10, 10, false);
            Car car2 = new Car(10, 10, 10, false);

            Driver Alex = new Driver("Alex", 0, car1, TeamColors.Red);
            Driver Ander = new Driver("Ander", 0, car2, TeamColors.Blue);
            
            Competition.
        }

    }
}
