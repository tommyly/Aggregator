using System.Collections.Generic;
using Proxy.Models;

namespace Proxy.Services.Interfaces
{
    public interface IErrorPoster
    {
        void Post(IEnumerable<string> urls, ErrorInformation errorInfo);
    }
}