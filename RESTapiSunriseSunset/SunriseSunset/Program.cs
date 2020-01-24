using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Console;

namespace SunriseSunset
{
    class SunMode
    {
        public string Status { get; set; }
        public Results Results { get; set; }
    }

    public class Results
    {
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
    class Program
    {
        static void Main()
        {
            string userCategoryUrl = "https://api.sunrise-sunset.org/json?lat=59.4370&lng=24.77536&date=today";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userCategoryUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                SunMode sunMode = JsonConvert.DeserializeObject<SunMode>(response);
                WriteLine($"API status: {sunMode.Status}");
                WriteLine($"Sunrise: {sunMode.Results.Sunrise}");
                WriteLine($"Sunset: {sunMode.Results.Sunset}");
            }
        }
    }
}
