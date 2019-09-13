using FlightApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Repositories
{
    public class AirportsRepository : IAirportsRepository
    {
        private Dictionary<string, Airport> AirportDictionary { get; set; }

        public AirportsRepository()
        {
            AirportDictionary = new Dictionary<string, Airport>();
        }

        public bool AddAirport(string name, string city, string country, string iata3, string latitude, string longitude)
        {
            var airport = new Airport(name, city, country, iata3, latitude, longitude);
            return AirportDictionary.TryAdd(iata3, airport);
        }

        public Airport GetAirport(string iata3)
        {
            return AirportDictionary.GetValueOrDefault(iata3, null);
        }
    }
}
