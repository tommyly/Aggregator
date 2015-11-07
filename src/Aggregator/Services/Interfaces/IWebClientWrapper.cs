using System.Collections.Specialized;

namespace Aggregator.Services.Interfaces
{
    public interface IWebClientWrapper
    {
        void UploadValues(string url, NameValueCollection values);
    }
}