using FlightApi.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightApiTests
{
    [TestClass]
    public class AirportsRepositoryTests
    {

        private IAirportsRepository _subject;

        [TestInitialize]
        public void Initialize()
        {
            _subject = new AirportsRepository();
        }

        [TestMethod]
        public void AddAirportTest()
        {
            var name = "airport1";
            var city = "city1";
            var country = "country1";
            var iata3 = "AAA";
            var lat = "1";
            var lon = "2";

            var result = _subject.AddAirport(name, city, country, iata3, lat, lon);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddAirportAlreadyExistsTest()
        {
            var name = "airport1";
            var city = "city1";
            var country = "country1";
            var iata3 = "AAA";
            var lat = "1";
            var lon = "2";

            var result = _subject.AddAirport(name, city, country, iata3, lat, lon);
            Assert.AreEqual(result, true);

            result = _subject.AddAirport(name, city, country, iata3, lat, lon);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GetAirportTest()
        {
            var name = "airport1";
            var city = "city1";
            var country = "country1";
            var iata3 = "AAA";
            var lat = "1";
            var lon = "2";

            _subject.AddAirport(name, city, country, iata3, lat, lon);
            var airport = _subject.GetAirport(iata3);

            Assert.AreEqual(airport.Name, name);
            Assert.AreEqual(airport.City, city);
            Assert.AreEqual(airport.Country, country);
            Assert.AreEqual(airport.Iata3, iata3);
            Assert.AreEqual(airport.Latitude, lat);
            Assert.AreEqual(airport.Longitude, lon);
        }

        [TestMethod]
        public void GetAirportDoesNotExistTest()
        {
            var airport = _subject.GetAirport("AAA");
            Assert.IsNull(airport);
        }
    }
}
