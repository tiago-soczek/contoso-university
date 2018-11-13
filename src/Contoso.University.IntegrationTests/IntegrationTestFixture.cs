using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Contoso.University.Api;

namespace Contoso.University.IntegrationTests
{
    public class IntegrationTestFixture : WebApplicationFactory<Startup>
    {
        public IntegrationTestFixture()
        {
            Client = CreateClient();
        }

        public HttpClient Client { get; }

        public TService GetService<TService>() => Server.Host.Services.GetRequiredService<TService>();

        protected override void Dispose(bool disposing)
        {
            Client?.Dispose();

            base.Dispose(disposing);
        }
    }
}
