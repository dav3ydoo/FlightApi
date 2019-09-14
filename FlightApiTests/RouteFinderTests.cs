using FlightApi;
using FlightApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightApiTests
{
    [TestClass]
    public class RouteFinderTests
    {
        private IRouteFinder _subject;
        Airport _airport1, _airport2, _airport3, _airport4, _airport5;

        [TestInitialize]
        public void Initialize()
        {
            _subject = new RouteFinder();
            InitializeTestData();
        }

        [TestMethod]
        public async Task FindShortestRouteTest()
        {
            _airport1.AddDepartingFlight(_airport2);
            _airport2.AddDepartingFlight(_airport3);
            _airport3.AddDepartingFlight(_airport4);
            _airport4.AddDepartingFlight(_airport5);

            var route = await _subject.FindShortestRouteAsync(_airport1, _airport5);

            Assert.AreEqual(route[0].Origin, _airport1);
            Assert.AreEqual(route[0].Destination, _airport2);
            Assert.AreEqual(route[1].Origin, _airport2);
            Assert.AreEqual(route[1].Destination, _airport3);
            Assert.AreEqual(route[2].Origin, _airport3);
            Assert.AreEqual(route[2].Destination, _airport4);
            Assert.AreEqual(route[3].Origin, _airport4);
            Assert.AreEqual(route[3].Destination, _airport5);

        }

        [TestMethod]
        public async Task FindShortestRouteNullOriginTest()
        {
            _airport1.AddDepartingFlight(_airport2);
            _airport2.AddDepartingFlight(_airport3);

            var route = await _subject.FindShortestRouteAsync(null, _airport3);
            Assert.IsNull(route);
        }

        [TestMethod]
        public async Task FindShortestRouteNullDestintionTest()
        {
            _airport1.AddDepartingFlight(_airport2);
            _airport2.AddDepartingFlight(_airport3);

            var route = await _subject.FindShortestRouteAsync(_airport1, null);
            Assert.IsNull(route);
        }

        [TestMethod]
        public async Task FindShortestRouteDirectFlightTest()
        {
            var flight1 = _airport1.AddDepartingFlight(_airport2);
            var route = await _subject.FindShortestRouteAsync(_airport1, _airport2);

            Assert.AreEqual(route[0].Origin, _airport1);
            Assert.AreEqual(route[0].Destination, _airport2);
        }

        [TestMethod]
        public async Task FindShortestRouteNoRouteTest()
        {
            _airport1.AddDepartingFlight(_airport2);
            _airport2.AddDepartingFlight(_airport3);

            var route = await _subject.FindShortestRouteAsync(_airport1, _airport5);
            Assert.IsNull(route);
        }

        [TestMethod]
        public async Task FindShortestRouteWithNoRouteTest()
        {
            var flight1 = _airport1.AddDepartingFlight(_airport2);
            var flight2 = _airport2.AddDepartingFlight(_airport3);
            var flight4 = _airport4.AddDepartingFlight(_airport5);

            var route = await _subject.FindShortestRouteAsync(_airport1, _airport5);
            Assert.IsNull(route);
        }

        [TestMethod]
        public async Task FindShortestRouteWithCycleTest()
        {
            var flight1 = _airport1.AddDepartingFlight(_airport2);
            var flight2 = _airport2.AddDepartingFlight(_airport3);
            var flight3 = _airport3.AddDepartingFlight(_airport1);
            var flight4 = _airport4.AddDepartingFlight(_airport5);

            var route = await _subject.FindShortestRouteAsync(_airport1, _airport5);
            Assert.IsNull(route);
        }

        private void InitializeTestData()
        {
            _airport1 = new Airport("airport1", "city1", "country1", "AAA", "1", "2");
            _airport2 = new Airport("airport2", "city2", "country2", "BBB", "3", "4");
            _airport3 = new Airport("airport3", "city3", "country3", "CCC", "5", "6");
            _airport4 = new Airport("airport4", "city4", "country4", "DDD", "7", "8");
            _airport5 = new Airport("airport5", "city5", "country5", "EEE", "9", "10");
        }
    }
}
