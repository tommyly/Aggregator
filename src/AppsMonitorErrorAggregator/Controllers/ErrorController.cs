using System.Web.Http;
using Aggregator.Models;
using Aggregator.Services.Interfaces;

namespace Aggregator.Controllers
{
    public class ErrorController : ApiController
    {
        private readonly IAggregatorConfiguration _configuration;
        private readonly IErrorPoster _errorPoster;

        public ErrorController(IAggregatorConfiguration configuration, IErrorPoster errorPoster)
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
