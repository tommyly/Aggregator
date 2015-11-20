using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Aggregator.Models;
using Aggregator.Services;
using Aggregator.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Aggregator.Tests.Service
{
    [TestFixture]
    public class GivenErrorInformation
    {
        private Mock<IWebClientWrapper> _webClientWrapper;
        private ErrorInformation _errorInfo;
        private List<string> _urls;

        [TestFixtureSetUp]
        public void WhenPostingErrorInformation()
        {
            _errorInfo = new ErrorInformation
            {
                ErrorId = "errorId",
                Error = "SomeError",
                InfoUrl = "http://error",
                SourceId = "srcId"
            };

            _urls = new List<string>
            {
                "url1",
                "url2"
            };

            _webClientWrapper = new Mock<IWebClientWrapper>();

            var errorPoster = new ErrorPoster(_webClientWrapper.Object);
            errorPoster.Post(_urls, _errorInfo);
        }

        [Test]
        public void ThenTheErrorInformationIsPostedToProvidedUrls()
        {
            foreach (var url in _urls)
            {
                _webClientWrapper.Verify(wc => wc.UploadValues(
                   url,
                   It.Is<NameValueCollection>(
                       c => c["errorId"] == _errorInfo.ErrorId &&
                            c["error"] == _errorInfo.Error &&
                            c["infoUrl"] == _errorInfo.InfoUrl &&
                            c["sourceId"] == _errorInfo.SourceId
                   )));
            }
        }
    }

    [TestFixture]
    public class GivenAHttp4xxOr5xxResponse
    {
        private Mock<IWebClientWrapper> _webClientWrapper;
        private ErrorInformation _errorInfo;
        private List<string> _urls;
        private int _errorCount;

        [TestFixtureSetUp]
        public void WhenPostingErrorInformation()
        {
            _errorInfo = new ErrorInformation
            {
                ErrorId = "errorId",
                Error = "SomeError",
                InfoUrl = "http://error",
                SourceId = "srcId"
            };

            _urls = new List<string>
            {
                "url1",
                "url2"
            };

            _errorCount = 0;

            _webClientWrapper = new Mock<IWebClientWrapper>();
            _webClientWrapper
                .Setup(wc => wc.UploadValues(It.IsAny<string>(), It.IsAny<NameValueCollection>()))
                .Callback(() =>
                {
                    if (_errorCount == 1)
                    {
                        throw new Exception();
                    }

                    _errorCount++;
                });

            var errorPoster = new ErrorPoster(_webClientWrapper.Object);
            errorPoster.Post(_urls, _errorInfo);
        }

        [Test]
        public void ThenAnAttemptToPostTheOtherErrorInformationIsMade()
        {
            _webClientWrapper.Verify(wc => wc.UploadValues(
                _urls[1],
                It.Is<NameValueCollection>(
                    c => c["errorId"] == _errorInfo.ErrorId &&
                        c["error"] == _errorInfo.Error &&
                        c["infoUrl"] == _errorInfo.InfoUrl &&
                        c["sourceId"] == _errorInfo.SourceId
                )));
        }
    }
}
