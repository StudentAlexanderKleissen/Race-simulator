﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get;set; }
        public int Points{ get;set; }
        public IEquipment Equipment { get;set; }
        public IParticipant.TeamColors TeamColor { get; set; }

        public Driver(string name, int points, IEquipment equipment,IParticipant.TeamColors teamcolor)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColor = teamcolor;
        }

        public Driver()
        {
        }

        public override string ToString()
        {
            return $"name = {this.Name}, point = {this.Points}";
        }

        //public static implicit operator Driver(DriversChangedEventArgs v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
