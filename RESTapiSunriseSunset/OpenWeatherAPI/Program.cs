using System;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Console;

namespace OpenWeatherAPI
{
    class WeatherDetails
    {
        //Parameters needed to get from the API
        /*public class Status
        {
            public string Cod { get; set; }
        }*/

        public class Coord
        {
            public string Lon { get; set; }
            public string lat { get; set; }
        }

        public class Weather
        {
            public string Main { get; set; }
            public string Description { get; set; }
        }

        public class Main
        {
            public float Temp { get; set; }
            public float Feels_like { get; set; }
            public int Pressure { get; set; }
            public byte Humidity { get; set; }
        }

        public class Sys
        {
            public string Country { get; set; }
        }

        //User for accessing all the previously defined parameters needed to get from API
        public class Root
        {
            //public Status Cod { get; set; }
            public Main Main { get; set; }
            public Sys Sys { get; set; }
            public List<Weather> Weather { get; set; }
            public string Name { get; set; }
        }
    }
    class Program
    {
        static void Main()
        {
            while (true)
            {
                WriteLine("Hello!");
                WriteLine("This is weather application based on OpenWeatherMap. \n");
            Again1:
                WriteLine("Insert city you want to know weather of:");
                string userChoice = ReadLine();
            Again2:
                string userAppId = "82bd466522316d8f8b2162f9063dfeb4";
                string location = userChoice;
                string url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + userAppId;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();

                using (var responseReader = new StreamReader(webStream))
                {
                    var response = responseReader.ReadToEnd();
                    WeatherDetails.Root weatherDetails = JsonConvert.DeserializeObject<WeatherDetails.Root>(response);
                    Clear();
                    //WriteLine($"\nAPI status: {weatherDetails}");
                    //SetCursorPosition(0, 0);
                    WriteLine($"\nCity: {weatherDetails.Name}");
                    WriteLine($"Location: {weatherDetails.Sys.Country}");
                    WriteLine($"\nWeather now: {weatherDetails.Weather[0].Main} - {weatherDetails.Weather[0].Description}");
                    WriteLine($"Temperature now: {Math.Round(weatherDetails.Main.Temp - 273.15, 1)}\u00B0C");
                    WriteLine($"Feels like: {Math.Round(weatherDetails.Main.Feels_like - 273.15, 1)}\u00B0C");
                    WriteLine($"Humidity: {weatherDetails.Main.Humidity} %");
                }

                WriteLine("\nCheck again or check other city?");
                WriteLine("Again = again | Other city = new");
                string lastUserCityChoise = userChoice;
                userChoice = ReadLine();

                if (userChoice.ToLower() == "again")
                {
                    Clear();
                    userChoice = lastUserCityChoise;
                    goto Again2;
                }

                else if (userChoice.ToLower() == "new")
                {
                    Clear();
                    goto Again1;
                }
            }
        }
    }
}
