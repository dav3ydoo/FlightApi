using FlightApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Repositories
{
    public interface IAirportsRepository
    {
        bool AddAirport(string name, string city, string country, string iata3, string latitude, string longitude);

        Airport GetAirport(string iata3);
    }
}
