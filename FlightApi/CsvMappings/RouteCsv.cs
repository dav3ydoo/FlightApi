using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.CsvMappings
{
    public class RouteCsv
    {
        private const string AIRLINE_ID_HEADER = "Airline Id";
        private const string ORIGIN_HEADER = "Origin";
        private const string DESTINATION_HEADER = "Destination";

        [CsvHelper.Configuration.Attributes.Name(AIRLINE_ID_HEADER)]
        public string AirlineId { get; set; }

        [CsvHelper.Configuration.Attributes.Name(ORIGIN_HEADER)]
        public string Origin { get; set; }

        [CsvHelper.Configuration.Attributes.Name(DESTINATION_HEADER)]
        public string Destination { get; set; }
    }
}
