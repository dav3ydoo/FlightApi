using FlightApi;
using FlightApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public void FindShortestRouteTest()
        {
            var flight1 = _airport1.AddFlight(_airport2);
            var flight2 = _airport2.AddFlight(_airport3);
            var flight3 =_airport3.AddFlight(_airport4);
            var flight4 =_airport4.AddFlight(_airport5);

            var route = _subject.FindShortestRoute(_airport1, _airport5);

            Assert.AreEqual(route[0], flight1);
            Assert.AreEqual(route[1], flight2);
            Assert.AreEqual(route[2], flight3);
            Assert.AreEqual(route[3], flight4);

        }

        [TestMethod]
        public void FindShortestRouteNullOriginTest()
        {
            _airport1.AddFlight(_airport2);
            _airport2.AddFlight(_airport3);

            var route = _subject.FindShortestRoute(null, _airport3);
            Assert.IsNull(route);
        }

        [TestMethod]
        public void FindShortestRouteNullDestintionTest()
        {
            _airport1.AddFlight(_airport2);
            _airport2.AddFlight(_airport3);

            var route = _subject.FindShortestRoute(_airport1, null);
            Assert.IsNull(route);
        }

        [TestMethod]
        public void FindShortestRouteDirectFlightTest()
        {
            var flight1 = _airport1.AddFlight(_airport2);
            var route = _subject.FindShortestRoute(_airport1, _airport2);

            Assert.AreEqual(route[0], flight1);
        }

        [TestMethod]
        public void FindShortestRouteNoRouteTest()
        {
            _airport1.AddFlight(_airport2);
            _airport2.AddFlight(_airport3);

            var route = _subject.FindShortestRoute(_airport1, _airport5);
            Assert.IsNull(route);
        }

        [TestMethod]
        public void FindShortestRouteWithNoRouteTest()
        {
            var flight1 = _airport1.AddFlight(_airport2);
            var flight2 = _airport2.AddFlight(_airport3);
            var flight4 = _airport4.AddFlight(_airport5);

            var route = _subject.FindShortestRoute(_airport1, _airport5);
            Assert.IsNull(route);
        }

        [TestMethod]
        public void FindShortestRouteWithCycleTest()
        {
            var flight1 = _airport1.AddFlight(_airport2);
            var flight2 = _airport2.AddFlight(_airport3);
            var flight3 = _airport3.AddFlight(_airport1);
            var flight4 = _airport4.AddFlight(_airport5);

            var route = _subject.FindShortestRoute(_airport1, _airport5);
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
