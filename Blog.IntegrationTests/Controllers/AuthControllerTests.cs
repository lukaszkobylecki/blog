using Blog.Infrastructure.Commands.Auth;
using Blog.Infrastructure.DTO;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Controllers
{
    public class AuthControllerTests : ControllerTestsBase
    {
        [Test]
        [TestCase("user1@email.com", "password")]
        [TestCase("   user1@email.com   ", "  password   ")]
        public async Task Login_ExistingUser_ShouldReturnJwtToken(string email, string password)
        {
            var command = new Login
            {
                Email = email,
                Password = password
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync("auth", payload);
            var jwt = await DeserializeAsync<JwtDto>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jwt.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        [TestCase("user1@email.com", "bad-password")]
        [TestCase("user1000@email.com", "password")]
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("  ", "    ")]
        public async Task Login_InvalidData_ShouldReturnBadRequest(string email, string password)
        {
            var command = new Login
            {
                Email = email,
                Password = password
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync("auth", payload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
