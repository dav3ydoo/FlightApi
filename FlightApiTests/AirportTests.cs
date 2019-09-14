using FlightApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightApiTests
{
    [TestClass]
    public class AirportTests
    {

        private Airport _subject;

        private const string AIRPORT_NAME = "airport1";
        private const string AIRPORT_CITY = "city1";
        private const string AIRPORT_COUNTRY = "country1";
        private const string AIRPORT_IATA3 = "AAA";
        private const string AIRPORT_LAT = "1";
        private const string AIRPORT_LON = "2";

        [TestInitialize]
        public void Initialize()
        {
            _subject = new Airport(AIRPORT_NAME, AIRPORT_CITY, AIRPORT_COUNTRY, AIRPORT_IATA3, AIRPORT_LAT, AIRPORT_LON);
        }

        [TestMethod]
        public void AddDepartingFlightTest()
        {
            var destination = new Airport("name2", "city2", "country2", "BBB", "3", "4");
            Assert.AreEqual(_subject.AddDepartingFlight(destination), true);
        }

        [TestMethod]
        public void AddDepartingFlightAlreadyExistsTest()
        {
            var destination = new Airport("name2", "city2", "country2", "BBB", "3", "4");
            Assert.AreEqual(_subject.AddDepartingFlight(destination), true);
            Assert.AreEqual(_subject.AddDepartingFlight(destination), false);
        }

        [TestMethod]
        public void GetDepartingFlightsTest()
        {
            var destination = new Airport("name2", "city2", "country2", "BBB", "3", "4");
            _subject.AddDepartingFlight(destination);
            Assert.AreEqual(_subject.GetDepartingFlights()[0].Destination, destination);
        }
    }
}
