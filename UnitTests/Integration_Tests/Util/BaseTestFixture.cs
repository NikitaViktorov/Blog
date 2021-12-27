using System.Net.Http;
using Blog;
using DAL.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Integration_Tests.Util
{
    internal class BaseTestFixture
    {
        public BaseTestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            DbContext = TestServer.Host.Services.GetService<BlogContext>();

            FakeDbInitializer.Initialize(DbContext);
        }

        public TestServer TestServer { get; set; }
        public BlogContext DbContext { get; }
        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}