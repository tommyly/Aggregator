using System.Collections.Generic;
using Aggregator.Models;

namespace Aggregator.Services.Interfaces
{
    public interface IErrorPoster
    {
        void Post(IEnumerable<string> urls, ErrorInformation errorInfo);
    }
}