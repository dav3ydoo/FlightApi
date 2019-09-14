using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Models
{
    public class Airport
    {
        // Departing flights by IATA 3 code.
        private Dictionary<string, Flight> DepartingFlightsDictionary;
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Iata3 { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }

        public Airport(string name, string city, string country, string iata3, string latitude, string longitude)
        {
            Name = name;
            City = city;
            Country = country;
            Iata3 = iata3;
            Latitude = latitude;
            Longitude = longitude;
            DepartingFlightsDictionary = new Dictionary<string, Flight>();
        }

        /// <summary>
        /// Adds departing flight to this airport instance.  
        /// </summary>
        /// <returns>True if added. False if already exists.</returns>
        public bool AddDepartingFlight(Airport destination)
        {
            return DepartingFlightsDictionary.TryAdd(destination.Iata3, new Flight(this, destination));
        }

        /// <summary>
        /// Gets list of departing flights for this airport instance.  
        /// </summary>
        /// <returns>List of departing flights.</returns>
        public List<Flight> GetDepartingFlights()
        {
            return DepartingFlightsDictionary.Values.ToList<Flight>();
        }
    }
}
