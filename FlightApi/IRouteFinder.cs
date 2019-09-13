using FlightApi.Models;
using System.Collections.Generic;

namespace FlightApi
{
    public interface IRouteFinder
    {
        List<Flight> FindShortestRoute(Airport origin, Airport destination);
    }
}