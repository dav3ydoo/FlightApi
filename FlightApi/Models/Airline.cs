﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Models
{
    public class Airline
    {
        public string Name { get; private set; }
        public string AirlineId { get; private set; }
        public string Iata3 { get; private set; }
        public string Country { get; private set; }

        public Airline(string name, string airlineId, string iata3, string country)
        {
            Name = name;
            AirlineId = airlineId;
            Iata3 = iata3;
            Country = country;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Airline a = (Airline)obj;
                return AirlineId == a.AirlineId;
            }
        }
        public override int GetHashCode()
        {
            return AirlineId.GetHashCode();
        }
    }
}
