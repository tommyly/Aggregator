using System.Collections.Specialized;

namespace Proxy.Services.Interfaces
{
    public interface IWebClientWrapper
    {
        void UploadValues(string url, NameValueCollection values);
    }
}