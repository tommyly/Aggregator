using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Proxy.Services.Interfaces;

namespace Proxy.Services
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        public IEnumerable<string> AppMonitorUrls { get; }
        public string ProxyUrl { get; private set; }

        public ProxyConfiguration()
        {
            AppMonitorUrls = ConfigurationManager.AppSettings["AppMonitorUrls"].Split(',').ToList();
            ProxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];
        }
    }
}
