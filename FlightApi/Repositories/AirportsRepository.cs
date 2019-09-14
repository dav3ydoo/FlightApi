using FlightApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Repositories
{
    public class AirportsRepository : IAirportsRepository
    {
        // Airports by IATA 3 code.
        private Dictionary<string, Airport> AirportDictionary { get; set; }

        public AirportsRepository()
        {
            AirportDictionary = new Dictionary<string, Airport>();
        }

        /// <summary>
        /// Adds airport to the airport dictionary which can be queried by the IATA 3 code.
        /// </summary>
        /// <param name="name">Airport name.</param>
        /// <param name="city">City airport is in.</param>
        /// <param name="country">Country airport is in.</param>
        /// <param name="iata3">IATA 3 code that uniquely identifies airport.</param>
        /// <param name="latitude">Latitude of airport.</param>
        /// <param name="longitude">Longitude of airport.</param>
        /// <returns>True if added.  False if airport with the same IATA 3 already exists.</returns>
        public bool AddAirport(string name, string city, string country, string iata3, string latitude, string longitude)
        {
            var airport = new Airport(name, city, country, iata3, latitude, longitude);
            return AirportDictionary.TryAdd(iata3, airport);
        }

        /// <summary>
        /// Get airport instance by IATA 3 code.
        /// </summary>
        /// <param name="iata3">IATA 3 code that uniquely represents the airport.</param>
        /// <returns>Airport if found.  Otherwise, null.</returns>
        public Airport GetAirport(string iata3)
        {
            return AirportDictionary.GetValueOrDefault(iata3, null);
        }
    }
}
