using System.Web.Http;
using Proxy.Models;
using Proxy.Services.Interfaces;

namespace Proxy.Controllers
{
    public class ErrorController : ApiController
    {
        private readonly IProxyConfiguration _configuration;
        private readonly IErrorPoster _errorPoster;

        public ErrorController(IProxyConfiguration configuration, IErrorPoster errorPoster)
        {
            _configuration = configuration;
            _errorPoster = errorPoster;
        }

        public IHttpActionResult Post(ErrorInformation errorInfo)
        {
            _errorPoster.Post(_configuration.AppMonitorUrls, errorInfo);

            return Ok();
        }
    }
}
