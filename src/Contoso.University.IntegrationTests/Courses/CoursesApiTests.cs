using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Contoso.University.Model.Courses.Commands;
using Xunit;

namespace Contoso.University.IntegrationTests.Courses
{
    public class CoursesApiTests : BaseTest
    {
        public CoursesApiTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Register_EmptyPayload()
        {
            var result = await Client.PostAsJsonAsync("/courses", new RegisterCourseCommand());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_ValidInfo()
        {
            var result = await Client.PostAsJsonAsync("/courses", new RegisterCourseCommand
            {
                Title = "CQRS",
                Description = "CQRS Course"
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
