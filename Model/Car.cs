using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Model
{
    public class Car : IEquipment
    {
        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            IsBroken = isBroken;
        }

        public int Quality { get; set; }
        public int Performance { get; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
        int IEquipment.Performance { get; set; }
    }
}
