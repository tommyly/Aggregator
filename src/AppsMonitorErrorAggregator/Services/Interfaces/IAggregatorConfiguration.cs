using System.Collections.Generic;

namespace Aggregator.Services.Interfaces
{
    public interface IAggregatorConfiguration
    {
        IEnumerable<string> AppMonitorUrls { get; }
    }
}