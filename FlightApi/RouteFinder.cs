using FlightApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi
{
    public class RouteFinder : IRouteFinder
    {
        /// <summary>
        /// Finds the shortest route between an origin and destination airport.
        /// </summary>
        /// <param name="origin">Departure airport.</param>
        /// <param name="destination">Arrival airport.</param>
        /// <returns>A list of flights in order representing the shortest route from the origin to the destination.</returns>
        public async Task<List<Flight>> FindShortestRouteAsync(Airport origin, Airport destination)
        {
            return await Task.Run(() => FindShortestRoute(origin, destination));
        }

        private List<Flight> FindShortestRoute(Airport origin, Airport destination)
        {
            if (origin == null || destination == null)
            {
                return null;
            }

            HashSet<Airport> visited = new HashSet<Airport>();
            Queue<List<Flight>> queue = new Queue<List<Flight>>();

            foreach (var flight in origin.GetDepartingFlights())
            {
                queue.Enqueue(new List<Flight>() { flight });
            }

            while (queue.Count > 0)
            {
                var currentRoute = queue.Dequeue();
                var lastFlightInCurrentRoute = currentRoute[currentRoute.Count - 1];

                if (lastFlightInCurrentRoute.Destination == destination)
                {
                    return currentRoute;
                }

                visited.Add(lastFlightInCurrentRoute.Origin);

                foreach (var flight in lastFlightInCurrentRoute.Destination.GetDepartingFlights())
                {
                    if (visited.Contains(flight.Destination)) continue;

                    var newRoute = new List<Flight>(currentRoute) { flight };
                    queue.Enqueue(newRoute);
                }
            }

            return null;
        }
    }
}
