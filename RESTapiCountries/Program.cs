using System;
using static System.Console;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace RESTapiCountries
{
    class RootObject
    {
        public class Currency
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
        }

        public class Language
        {
            public string Iso639_1 { get; set; }
            public string Iso639_2 { get; set; }
            public string Name { get; set; }
            public string NativeName { get; set; }
        }

        public class Translations
        {
            public string De { get; set; }
            public string Es { get; set; }
            public string Fr { get; set; }
            public string Ja { get; set; }
            public string It { get; set; }
            public string Br { get; set; }
            public string Pt { get; set; }
            public string Nl { get; set; }
            public string Hr { get; set; }
            public string Fa { get; set; }
        }

        public class RegionalBloc
        {
            public string Acronym { get; set; }
            public string Name { get; set; }
            public List<object> OtherAcronyms { get; set; }
            public List<object> OtherNames { get; set; }
        }

        public class Root
        {
            public string Name { get; set; }
            public List<string> TopLevelDomain { get; set; }
            public string Alpha2Code { get; set; }
            public string Alpha3Code { get; set; }
            public List<string> CallingCodes { get; set; }
            public string Capital { get; set; }
            public List<string> AltSpellings { get; set; }
            public string Region { get; set; }
            public string Subregion { get; set; }
            public int Population { get; set; }
            public List<double> Latlng { get; set; }
            public string Demonym { get; set; }
            public double Area { get; set; }
            public double Gini { get; set; }
            public List<string> Timezones { get; set; }
            public List<string> Borders { get; set; }
            public string NativeName { get; set; }
            public string NumericCode { get; set; }
            public List<Currency> Currencies { get; set; }
            public List<Language> Languages { get; set; }
            public Translations Translations { get; set; }
            public string Flag { get; set; }
            public List<RegionalBloc> RegionalBlocs { get; set; }
            public string Cioc { get; set; }
        }
    }
    class Program
    {
        static void Main()
        {
            string RESTurl = "https://restcountries.eu/rest/v2/name/eesti";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RESTurl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using var responseStream = new StreamReader(webStream);
            var response = responseStream.ReadToEnd();

            //Since starting point of JSON is [0]: which is easily shown in mozilla under JSON table, the JSON parsin must be done directly to list
            List<RootObject.Root> countryDetails = JsonConvert.DeserializeObject<List<RootObject.Root>>(response);
            WriteLine($"Country name:       {countryDetails[0].Name}");
            WriteLine($"Country cioc:       {countryDetails[0].Cioc}");
            WriteLine($"Country domain:     {countryDetails[0].TopLevelDomain[0]}");
            WriteLine($"Country capital:    {countryDetails[0].Capital}");
            WriteLine($"Country region:     {countryDetails[0].Region}");
            WriteLine($"Country population: {countryDetails[0].Population}");
            WriteLine($"Country language:   {countryDetails[0].Languages[0].Name}");
            WriteLine($"Country borders:    {countryDetails[0].Borders[0]}");
            WriteLine($"Country borders:    {countryDetails[0].Borders[1]}");
        }
    }
}