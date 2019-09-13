using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FlightApi.CsvMappings;
using FlightApi.Repositories;

namespace FlightApi
{
    public class Startup
    {
        private const string AIRLINES_CSV_LOCATION = "..\\data\\full\\airlines.csv";
        private const string AIRPORTS_CSV_LOCATION = "..\\data\\full\\airports.csv";
        private const string ROUTES_CSV_LOCATION = "..\\data\\full\\routes.csv";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IAirportsRepository), typeof(AirportsRepository));
            services.AddTransient(typeof(IRouteFinder), typeof(RouteFinder));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            IAirportsRepository airportsRepository = app.ApplicationServices.GetService<IAirportsRepository>();

            AddTestData( airportsRepository);

            app.UseMvc();

        }

        private void AddTestData(IAirportsRepository airportsRepository)
        {
            var airportsReader = new StreamReader(AIRPORTS_CSV_LOCATION);
            var airportsCsv = new CsvReader(airportsReader);
            var airportsCsvRecords = airportsCsv.GetRecords<AirportCsv>();

            foreach (var record in airportsCsvRecords)
            {
                airportsRepository.AddAirport(record.Name, record.City, record.Country, record.ThreeDigitCode, record.Latitude, record.Longitude);
            }

            var routesReader = new StreamReader(ROUTES_CSV_LOCATION);
            var routesCsv = new CsvReader(routesReader);
            var routesCsvRecords = routesCsv.GetRecords<RouteCsv>();

            foreach (var record in routesCsvRecords)
            {
                var originAirport = airportsRepository.GetAirport(record.Origin);
                var destinationAirport = airportsRepository.GetAirport(record.Destination);
                originAirport.AddFlight(destinationAirport);
            }
        }
    }
}
