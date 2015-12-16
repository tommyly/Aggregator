using System.Collections.Generic;

namespace Proxy.Services.Interfaces
{
    public interface IProxyConfiguration
    {
        IEnumerable<string> AppMonitorUrls { get; }
    }
}