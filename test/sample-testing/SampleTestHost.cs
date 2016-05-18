using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using sample_routing;
using Xunit;

namespace sample_testing
{
    public class SampleTestHost
    {
        private TestServer _server;
        private HttpClient _client;

        public SampleTestHost()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            _server = new TestServer(host
                );
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task GetRoot_ReturnsHelloWorld()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello, World!",
                responseString);

        }
    }
}
