using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.Models
{
    public class Flight
    {
        public Flight(Airport origin, Airport destination)
        {
            Origin = origin;
            Destination = destination;
        }

        public Airport Origin { get; private set; }

        public Airport Destination { get; private set; }
    }
}
