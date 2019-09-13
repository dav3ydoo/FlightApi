using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightApi.CsvMappings
{
    public class AirlineCsv
    {
        private const string NAME_HEADER = "Name";
        private const string TWO_DIGIT_CODE_HEADER = "2 Digit Code";
        private const string THREE_DIGIT_CODE_HEADER = "3 Digit Code";
        private const string COUNTRY_HEADER = "Country";


        [CsvHelper.Configuration.Attributes.Name(NAME_HEADER)]
        public string Name { get; set; }

        [CsvHelper.Configuration.Attributes.Name(TWO_DIGIT_CODE_HEADER)]
        public string TwoDigitCode { get; set; }

        [CsvHelper.Configuration.Attributes.Name(THREE_DIGIT_CODE_HEADER)]
        public string ThreeDigitCode { get; set; }

        [CsvHelper.Configuration.Attributes.Name(COUNTRY_HEADER)]
        public string Country { get; set; }
    }
}
