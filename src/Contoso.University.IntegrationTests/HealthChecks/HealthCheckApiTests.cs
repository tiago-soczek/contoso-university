using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.University.IntegrationTests.HealthChecks
{
    public class HealthCheckApiTests : BaseTest
    {
        public HealthCheckApiTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Healthz()
        {
            var result = await Client.GetAsync("/_healthz");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
