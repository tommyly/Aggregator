using System;
using Aggregator.Services;
using Microsoft.Owin.Hosting;

namespace Aggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new AggregatorConfiguration();

            WebApp.Start<Startup>(config.AggregatorUrl);
            Console.ReadKey();
        }
    }
}
