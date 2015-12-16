using System;
using System.Reflection;
using log4net;
using Microsoft.Owin.Hosting;
using Proxy.Services;

namespace Proxy
{
    public class ErrorProxyService
    {
        private IDisposable _apiHost;
        public static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Start()
        {
            Logger.Info("Starting service ...");
            var config = new ProxyConfiguration();
            Logger.InfoFormat("Service is hosted on {0}", config.ProxyUrl);
            _apiHost = WebApp.Start<Startup>(config.ProxyUrl);
        }

        public void Stop()
        {
            Logger.Info("Stopping service...");
            _apiHost.Dispose();
        }
    }
}   
