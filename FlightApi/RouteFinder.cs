using FlightApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi
{
    public class RouteFinder : IRouteFinder
    {
        public List<Flight> FindShortestRoute(Airport origin, Airport destination)
        {
            HashSet<Airport> visited = new HashSet<Airport>();
            Queue<List<Flight>> queue = new Queue<List<Flight>>();

            foreach(var flight in origin.GetFlights())
            {
                queue.Enqueue(new List<Flight>(){flight});
            }

            while (queue.Count > 0)
            {
                var currentRoute = queue.Dequeue();
                var lastFlightInRoute = currentRoute[currentRoute.Count - 1];

                if (lastFlightInRoute.Destination == destination)
                {
                    return currentRoute;
                }

                visited.Add(lastFlightInRoute.Origin);

                foreach (var flight in lastFlightInRoute.Destination.GetFlights())
                {
                    if (visited.Contains(flight.Destination)) continue;

                    var newRoute = new List<Flight>(currentRoute){flight};
                    queue.Enqueue(newRoute);
                }
            }

            return null;
        }
    }
}
