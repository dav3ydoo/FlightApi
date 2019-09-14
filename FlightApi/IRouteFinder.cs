using FlightApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightApi
{
    public interface IRouteFinder
    {
        Task<List<Flight>> FindShortestRouteAsync(Airport origin, Airport destination);
    }
}