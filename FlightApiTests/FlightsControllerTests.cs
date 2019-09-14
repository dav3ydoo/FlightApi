using FlightApi.Controllers;
using FlightApi.Repositories;
using FlightApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using FlightApi.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightApiTests
{
    [TestClass]
    public class FlightsControllerTests
    {
        private FlightsController _subject;
        private IAirportsRepository _airportsRepository;
        private IRouteFinder _routeFinder;

        [TestInitialize]
        public void Initialize()
        {
            _airportsRepository = Substitute.For<IAirportsRepository>();
            _routeFinder = Substitute.For<IRouteFinder>();
            _subject = new FlightsController(_airportsRepository, _routeFinder);
        }

        [TestMethod]
        public async Task FindRouteTest()
        {
            var origin = "AAA";
            var destination = "BBB";

            var originAirport = new Airport("airport1", "city1", "country1", origin, "1", "2");
            var destinationAirport = new Airport("airport2", "city2", "country2", destination, "3", "4");

            _airportsRepository.GetAirport(origin).Returns(originAirport);
            _airportsRepository.GetAirport(destination).Returns(destinationAirport);

            var flightList = new List<Flight> { new Flight(originAirport, destinationAirport) };

            _routeFinder.FindShortestRouteAsync(originAirport, destinationAirport).Returns(flightList);

            var expectedValue = JsonConvert.SerializeObject(flightList);

            var objectResult = await _subject.Get(origin, destination) as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.AreEqual(objectResult.Value, expectedValue);
        }

        [TestMethod]
        public async Task FindRouteInvalidOriginTest()
        {
            var origin = "AAA";
            var destination = "BBB";

            var originAirport = new Airport("airport1", "city1", "country1", origin, "1", "2");
            var destinationAirport = new Airport("airport2", "city2", "country2", destination, "3", "4");

            _airportsRepository.GetAirport(origin).Returns((Airport)null);
            _airportsRepository.GetAirport(destination).Returns(destinationAirport);

            var objectResult = await _subject.Get(origin, destination) as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, 400);
            Assert.AreEqual(objectResult.Value, "Invalid origin [" + origin + "].");
        }

        [TestMethod]
        public async Task FindRouteInvalidDestinationTest()
        {
            var origin = "AAA";
            var destination = "BBB";

            var originAirport = new Airport("airport1", "city1", "country1", origin, "1", "2");
            var destinationAirport = new Airport("airport2", "city2", "country2", destination, "3", "4");

            _airportsRepository.GetAirport(origin).Returns(originAirport);
            _airportsRepository.GetAirport(destination).Returns((Airport)null);

            var objectResult = await _subject.Get(origin, destination) as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, 400);
            Assert.AreEqual(objectResult.Value, "Invalid destination [" + destination + "].");
        }

        [TestMethod]
        public async Task FindRouteWhereRouteIsNotFoundTest()
        {
            var origin = "AAA";
            var destination = "BBB";

            var originAirport = new Airport("airport1", "city1", "country1", origin, "1", "2");
            var destinationAirport = new Airport("airport2", "city2", "country2", destination, "3", "4");

            _airportsRepository.GetAirport(origin).Returns(originAirport);
            _airportsRepository.GetAirport(destination).Returns(destinationAirport);

            _routeFinder.FindShortestRouteAsync(originAirport, destinationAirport).Returns((List<Flight>)null);

            var objectResult = await _subject.Get(origin, destination) as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, 404);
            Assert.AreEqual(objectResult.Value, "No route exists between origin [" + origin + "] and destination [" + destination + "].");
        }
    }
}
