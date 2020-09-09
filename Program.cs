using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace CurrencyCourses
{
    internal class Program
    {
        private static void Main()
        {
            Core.MainOutput();
            Console.ReadKey();
        }
    }

    public class Core
    {
        public class Root
        {
            public Dictionary<string, double> Rates = new Dictionary<string, double>();
            public string Base { get; set; }
            public string date { get; set; }
        }

        public class Deserializer
        {
            public Deserializer()
            {
                defaultValute = ""; defaultValuteIsEnabled = false;
            }

            public Deserializer(string defaultValute)
            {
                this.defaultValute = defaultValute; defaultValuteIsEnabled = true;
            }

            private bool defaultValuteIsEnabled;
            private string defaultValute;

            public Dictionary<string, double> mainDictionary(string defaultValute, bool defaultValuteIsEnabled)
            {
                if (defaultValuteIsEnabled)
                {
                    Root Valuelist = new Root();
                    string json = APIResponse(defaultValute);
                    Valuelist = JsonConvert.DeserializeObject<Root>(json);
                    return Valuelist.Rates;
                }
                else
                {
                    Root Valuelist = new Root();
                    string json = APIResponse();
                    Valuelist = JsonConvert.DeserializeObject<Root>(json);
                    return Valuelist.Rates;
                }
            }

            //полный буллщит, но по другому я не сообразил xD
            public Dictionary<string, double> Returner()
            {
                return mainDictionary(defaultValute, defaultValuteIsEnabled);
            }

            private static string APIResponse(string defaultValute) //получение json из API ExchangeRates для получения курсов валют относительно выбранной валюты
            {
                string Url = "https://api.exchangeratesapi.io/latest?base=" + defaultValute;
                var WebClient = new WebClient();
                string Response = WebClient.DownloadString(Url);
                return Response;
            }

            private static string APIResponse() //получение json из API ExchangeRates для получения списка валют
            {
                string Url = "https://api.exchangeratesapi.io/latest";
                var WebClient = new WebClient();
                string Response = WebClient.DownloadString(Url);
                return Response;
            }
        }

        public static void MainOutput()//вывод
        {
            //Вывод шапки
            DateTime dateTime = DateTime.Now;
            Console.WriteLine($"Привет, я занимаюсь показом курсов валют, относительно выбранной валюты.\nCегодня {dateTime.ToString()}");
            Console.WriteLine("Для того чтобы начать работать, необходимо указать дефолтную валюту и валюту, которую необходимо узнать.\nСписок доступных валют:");
            Deserializer list = new Deserializer();
            Dictionary<string, double> Valuelist = list.Returner();

            foreach (KeyValuePair<string, double> kvp in Valuelist)
            {
                Console.Write($"{kvp.Key}\t");
            }
            // Установка дефолтной валюты
            while (true)
            {
                Console.Write("\nДефолтная валюта: ");
                string defaultValute = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(defaultValute))
                {
                    Console.WriteLine("Валюта не выбрана.");
                    continue;
                }
                else if (Valuelist.ContainsKey(defaultValute) || defaultValute == "EUR") //костыль, ибо EUR нет в дикте, т.к евро является дефолтным значением возвращаемым апи
                {
                    Deserializer dict = new Deserializer(defaultValute);
                    Dictionary<string, double> Courses = dict.Returner();
                    //выбор валюты для вывода значения
                    while (true)
                    {
                        Console.WriteLine("\nВведите необходимую валюту, или нажмите Enter, чтобы получить список всех актуальных курсов.");
                        string valute = Console.ReadLine().ToUpper();
                        if (string.IsNullOrWhiteSpace(valute))
                        {
                            //не вывод, а порнография какая-то ей богу блять, потом переделать
                            foreach (KeyValuePair<string, double> kvp in Courses)
                            {
                                Console.WriteLine($"\t{kvp.Key} - {Math.Round(kvp.Value, 2)}");
                            }
                            break;
                        }
                        Console.WriteLine($"{valute} value = {Math.Round(Courses[valute], 2)}.");
                        break;
                    }
                }
                else if (!Valuelist.ContainsKey(defaultValute))
                {
                    Console.WriteLine("Валюта не найдена.");
                    continue;
                }
                break;
            }
        }
    }
}