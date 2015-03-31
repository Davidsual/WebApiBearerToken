using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Davide.Trotta.WebApi.Server.Model;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;


namespace Davide.Trotta.WebApi.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = WebApp.Start<Startup>(url: "http://localhost:9000/");
            Console.WriteLine("Web API listening at http://localhost:9000/");

            Console.ReadLine();
        }
    }
}
