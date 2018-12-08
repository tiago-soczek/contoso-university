using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Contoso.University.Model.Students.Commands;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Contoso.University.IntegrationTests.Students
{
    public class StudentsApiTests : BaseTest
    {
        public StudentsApiTests(IntegrationTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Register_EmptyPayload_BadRequest()
        {
            var result = await Client.PostAsJsonAsync("/students", new RegisterStudentCommand());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_NullAddress_BadRequest()
        {
            var result = await Client.PostAsJsonAsync("/students", new RegisterStudentCommand
            {
                Name = "Tiago",
                Birthday = new DateTime(1987, 10, 03)
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_EmptyAddress_BadRequest()
        {
            var result = await Client.PostAsJsonAsync("/students", new RegisterStudentCommand
            {
                Name = "Tiago",
                Birthday = new DateTime(1987, 10, 03),
                Address = new RegisterStudentCommand.StudentAddress()
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_Complete_OK()
        {
            var result = await Client.PostAsJsonAsync("/students", new RegisterStudentCommand
            {
                Name = "Tiago",
                Birthday = new DateTime(1987, 10, 03),
                Address = new RegisterStudentCommand.StudentAddress
                {
                    Line1 = "A",
                    Line2 = "B",
                    City = "C",
                    Country = "X",
                    ZipCode = "X",
                    State = "X",
                }
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
