using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.CsvMappings
{
    public class AirportCsv
    {
        private const string NAME_HEADER = "Name";
        private const string CITY_HEADER = "City";
        private const string COUNTRY_HEADER = "Country";
        private const string IATA3_HEADER = "IATA 3";
        private const string LATITUDE_HEADER = "Latitute";
        private const string LONGITUDE_HEADER = "Longitude";

        [CsvHelper.Configuration.Attributes.Name(NAME_HEADER)]
        public string Name { get; set; }

        [CsvHelper.Configuration.Attributes.Name(CITY_HEADER)]
        public string City { get; set; }

        [CsvHelper.Configuration.Attributes.Name(COUNTRY_HEADER)]
        public string Country { get; set; }

        [CsvHelper.Configuration.Attributes.Name(IATA3_HEADER)]
        public string ThreeDigitCode { get; set; }

        [CsvHelper.Configuration.Attributes.Name(LATITUDE_HEADER)]
        public string Latitude { get; set; }

        [CsvHelper.Configuration.Attributes.Name(LONGITUDE_HEADER)]
        public string Longitude { get; set; }
    }
}
