using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace CurrencyCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.MainOutput();
            Console.ReadKey();
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

        public static void MainOutput()//Десериализация и вывод
        {
            Root dict = new Root();
            string json = Core.APIResponse();
            dict = JsonConvert.DeserializeObject<Root>(json);
            //Вывод шапки
            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"Привет, я занимаюсь показом курсов валют, относительно доллара США.\nCегодня {dateTime.ToString()}");
            Console.WriteLine("Для того чтобы начать работать, необходимо указать валюту, курс которой вам необходимо узнать.\nСписок доступных валют:");
            foreach (KeyValuePair<string, string> kvp in dict.Rates)
            {
                Console.Write($"{kvp.Key}\t");
            }
            while (true) 
            {
                Console.WriteLine("\nВведите необходимую валюту, или нажмите Enter, чтобы получить список всех актуальных курсов.");
                string valute = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(valute))
                {
                    foreach (KeyValuePair<string, string> kvp in dict.Rates)
                    {
                        Console.WriteLine($"{kvp.Key}\t{kvp.Value}");
                    }
                    break;
                }
                //Поиск непосредственно по ключу
                try
                {
                    Console.WriteLine($"{valute} value = {dict.Rates[valute]}.");
                    break;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Валюта {valute} не найдена в списке.");
                    continue;
                }
            }
        }
    }

    public class Root
    {
        public Dictionary<string, string> Rates = new Dictionary<string, string>();
        public string Base { get; set; }
        public string date { get; set; }

    }

}
