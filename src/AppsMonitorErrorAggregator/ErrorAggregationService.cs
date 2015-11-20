using System;
using System.Reflection;
using Aggregator.Services;
using log4net;
using Microsoft.Owin.Hosting;

namespace Aggregator
{
    public class ErrorAggregationService
    {
        private IDisposable _apiHost;
        public static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Start()
        {
            Logger.Info("Starting service ...");
            var config = new AggregatorConfiguration();
            Logger.InfoFormat("Service is hosted on {0}", config.AggregatorUrl);
            _apiHost = WebApp.Start<Startup>(config.AggregatorUrl);
        }

        public void Stop()
        {
            Logger.Info("Stopping service...");
            _apiHost.Dispose();
        }
    }
}   
