using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Models
{
    public class Airport
    {
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Iata3 { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }

        private Dictionary<string, Flight> DestinationToFlightDictionary;

        public Airport(string name, string city, string country, string iata3, string latitude, string longitude)
        {
            Name = name;
            City = city;
            Country = country;
            Iata3 = iata3;
            Latitude = latitude;
            Longitude = longitude;
            DestinationToFlightDictionary = new Dictionary<string, Flight>();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Airport a = (Airport)obj;
                return Iata3 == a.Iata3;
            }
        }

        public override int GetHashCode()
        {
            return Iata3.GetHashCode();
        }

        public void AddFlight(Airport destination)
        {
            var flight = new Flight(this, destination);
            DestinationToFlightDictionary.TryAdd(destination.Iata3, flight);
        }

        public List<Flight> GetFlights()
        {
            return DestinationToFlightDictionary.Values.ToList<Flight>();
        }
    }
}
