using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FlightApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IRouteFinder _routeFinder;
        private readonly IAirportsRepository _airportsRepository;

        public FlightsController(IAirportsRepository airportsRepository, IRouteFinder routeFinder)
        {
            _routeFinder = routeFinder;
            _airportsRepository = airportsRepository;
        }

        // TODO: make async.
        // GET api/find_route?origin={origin}&destination={destination}
        [HttpGet("find_route")]
        public ActionResult<string> Get(string origin, string destination)
        {
            var originAirport = _airportsRepository.GetAirport(origin);

            if (originAirport == null)
            {
                return BadRequest("Invalid origin [" + origin + "].");
            }

            var destinationAirport = _airportsRepository.GetAirport(destination);

            if (destinationAirport == null)
            {
                return BadRequest("Invalid destination [" + destination + "].");
            }

            var shortestRoute = _routeFinder.FindShortestRoute(originAirport, destinationAirport);

            if (shortestRoute == null)
            {
                return NotFound("No route exists between origin [" + origin + "] and destination [" + destination + "].");
            }

            return JsonConvert.SerializeObject(shortestRoute);
        }
    }
}
