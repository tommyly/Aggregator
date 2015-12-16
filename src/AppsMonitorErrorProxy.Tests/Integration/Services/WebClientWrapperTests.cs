using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Proxy.Services;

namespace Proxy.Tests.Integration.Services
{
    [TestFixture]
    public class GivenAUrlAndSomeValues
    {
        private string _url;
        private string _actualUrl = "";
        private List<string> _actualPostValues = new List<string>();

        [TestFixtureSetUp]
        public void WhenPostingToUrl()
        {

            _url = "http://localhost/integrationtests/";
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add(_url);
            httpListener.Start();

            Task.Run(() => GetPostData(httpListener));

            var values = new NameValueCollection
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"}
            };
            var webClientWrapper = new WebClientWrapper();
            webClientWrapper.UploadValues(_url, values);
        }

        [Test]
        public void ThenAPostRequestIsMadeToTheSpecifiedUrl()
        {
            Assert.That(_actualUrl, Is.EqualTo(_url));
        }

        [Test]
        public void ThenTheValuesArePosted()
        {
            Assert.That(_actualPostValues.Count, Is.EqualTo(2));

            Assert.That(_actualPostValues[0], Is.EqualTo("Key1=Value1"));
            Assert.That(_actualPostValues[1], Is.EqualTo("Key2=Value2"));
        }

        private void GetPostData(HttpListener httpListener)
        {
            var context = httpListener.GetContext();
            var request = context.Request;

            _actualUrl = request.Url.AbsoluteUri;

            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                _actualPostValues = reader.ReadToEnd().Split('&').ToList();
            }

            var response = context.Response;
            response.StatusCode = 200;
           
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("");
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
