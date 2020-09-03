using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;

namespace CurrencyCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(Core.APIResponse());
            foreach(KeyValuePair<string,object> kvp in dict)
            {

            }

            //Dictionary<string, Dictionary<string, double>> FullDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>();
        }
        
    }

    public class Core
    {
        public static string APIResponse() //получение json из API ExchangeRates
        {
            string Url = "https://api.exchangeratesapi.io/latest?base=USD";
            var WebClient = new WebClient();
            string Response = WebClient.DownloadString(Url);
            return Response;
        }
        public static void Deserialize()
        {
            Root root = JsonConvert.DeserializeObject<Root>(APIResponse());
        }
        public static void MainOutput(string Valute)
        {

            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"Привет, я занимаюсь показом курсов валют, относительно доллара США.\nCегодня {dateTime.ToString()}");
            Console.WriteLine("Для того чтобы начать работать, необходимо указать валюту, курс которой вам необходимо узнать.\nСписок доступных валют:");
        }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Rates
    {
        public double CAD { get; set; }
        public double HKD { get; set; }
        public double ISK { get; set; }
        public double PHP { get; set; }
        public double DKK { get; set; }
        public double HUF { get; set; }
        public double CZK { get; set; }
        public double GBP { get; set; }
        public double RON { get; set; }
        public double SEK { get; set; }
        public double IDR { get; set; }
        public double INR { get; set; }
        public double BRL { get; set; }
        public double RUB { get; set; }
        public double HRK { get; set; }
        public double JPY { get; set; }
        public double THB { get; set; }
        public double CHF { get; set; }
        public double EUR { get; set; }
        public double MYR { get; set; }
        public double BGN { get; set; }
        public double TRY { get; set; }
        public double CNY { get; set; }
        public double NOK { get; set; }
        public double NZD { get; set; }
        public double ZAR { get; set; }
        public double USD { get; set; }
        public double MXN { get; set; }
        public double SGD { get; set; }
        public double AUD { get; set; }
        public double ILS { get; set; }
        public double KRW { get; set; }
        public double PLN { get; set; }
    }

    public class Root
    {
        public Rates Rates { get; set; }
        public string Base { get; set; }
        public string date { get; set; }

    }

}
