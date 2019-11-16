using Blog.Infrastructure.Commands.Users;
using Blog.Infrastructure.DTO;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Controllers
{
    [TestFixture]
    public class UserControllerTests : ControllerTestsBase
    {
        [Test]
        public async Task GetUsers_ShouldReturnData()
        {
            var result = await GetResource<IEnumerable<UserDto>>("user");

            result.Response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            result.Data.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetUser_NotExisting_ShouldReturnNotFound()
        {
            var result = await GetResource<UserDto>("user/123");

            result.Response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetUser_Existing_ShouldReturnUser()
        {
            var id = 1;

            var result = await GetResource<UserDto>($"user/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
        }

        [Test]
        public async Task CreateUser_NotExisting_ShouldBeCreated()
        {
            var id = 11;
            var command = new CreateUser
            {
                Email = "  user11@email.com  ",
                Password = "password",
                Username = "  user11  "
            };
            var payload = GetPayload(command);
            var now = DateTime.UtcNow;
            
            var response = await Client.PostAsync("user", payload);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().Be($"user/{id}");

            var result = await GetResource<UserDto>($"user/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(11);
            result.Data.Email.Should().Be(command.Email.Trim());
            result.Data.Username.Should().Be(command.Username.Trim());
            result.Data.CreatedAt.Should().BeAfter(now);
            result.Data.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public async Task CreateUser_Existing_ShouldReturnBadRequest()
        {
            var command = new CreateUser
            {
                Email = "user1@email.com",
                Password = "password",
                Username = "user1"
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync("user", payload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteUser_Unauthorized_ShouldReturnUnauthorized()
        {
            var response = await Client.DeleteAsync($"user/100");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task DeleteUser_NotExisting_ShouldReturnBadRequest()
        {
            var id = 100;

            await AddAuthTokenAsync("user1@email.com", "password");
            var response = await Client.DeleteAsync($"user/{id}");
            RemoveAuthToken();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteUser_Existing_ShouldSuccess()
        {
            var id = 10;

            await AddAuthTokenAsync("user1@email.com", "password");
            var response = await Client.DeleteAsync($"user/{id}");
            RemoveAuthToken();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var result = await GetResource<UserDto>($"user/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
