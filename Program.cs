using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System.Xml;
using System.Net;

namespace CurrencyCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = new DateTime();
            date = DateTime.Today;

            XMLParser Answer = new XMLParser();
            Console.WriteLine(Answer.Request("28", "08", "2020"));

        }
    }

    class XMLParser
    {
        public string Request(string dd, string mm, string yyyy)
        {
            string Url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + dd + "/" + mm + "/" + yyyy;
            Console.WriteLine(Url);
            var WebClient = new WebClient();
            string Response = WebClient.DownloadString(Url);
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Encoding cp1251 = Encoding.GetEncoding(1251);
            //Encoding utf8 = Encoding.UTF8;
            //byte[] cpbytes = cp1251.GetBytes(WebClient.DownloadString(Url));
            //byte[] utfbytes = Encoding.Convert(cp1251, utf8, cpbytes);

            //char[] utfchars = new char[utf8.GetCharCount(utfbytes, 0, utfbytes.Length)];
            //utf8.GetChars(utfbytes, 0, utfbytes.Length, utfchars, 0);
            //string Response = new string(utfchars);
            //Console.WriteLine(Response);
            return Response;
        }

        //public string RequestXML(string dd, string mm, string yyyy)
        //{
        //    string Url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + dd + "/" + mm + "/" + yyyy;
        //    var WebClient = new WebClient();
        //    XmlDocument source = new XmlDocument();
        //    source.LoadXml(WebClient.DownloadString(Url));
        //    Console.WriteLine(source);
        //    return null;
        //}
    }
}
