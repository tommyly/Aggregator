using System;
using Aggregator.Services;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Aggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<ErrorAggregationService>(s =>                        
                {
                    s.ConstructUsing(name => new ErrorAggregationService());     
                    s.WhenStarted(aggregator => aggregator.Start());             
                    s.WhenStopped(aggregator => aggregator.Stop());              
                });

                x.RunAsLocalSystem();                            

                x.SetDescription("Apps Monitor Error Aggregation Service");        
                x.SetDisplayName("AppsMonitor.ErrorAggregator");                       
                x.SetServiceName("AppsMonitor.ErrorAggregator");                        
            });
        }
    }

    public class ErrorAggregationService
    {
        private IDisposable _apiHost;

        public void Start()
        {
            var config = new AggregatorConfiguration();

            _apiHost = WebApp.Start<Startup>(config.AggregatorUrl);
        }

        public void Stop()
        {
            _apiHost.Dispose();
        }
    }
}
