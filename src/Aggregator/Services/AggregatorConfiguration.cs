using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Aggregator.Services.Interfaces;

namespace Aggregator.Services
{
    public class AggregatorConfiguration : IAggregatorConfiguration
    {
        public IEnumerable<string> AppMonitorUrls { get; private set; }
        public string AggregatorUrl { get; private set; }

        public AggregatorConfiguration()
        {
            AppMonitorUrls = ConfigurationManager.AppSettings["AppMonitorUrls"].Split(',').ToList();
            AggregatorUrl = ConfigurationManager.AppSettings["AggregatorUrl"];
        }
    }
}
