using Blog.Common.Helpers;
using Blog.Infrastructure.Commands.User;
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
        public UserControllerTests()
        {
            BaseUrl = "user";
        }

        [Test]
        public async Task GetUsers_ShouldReturnData()
        {
            var result = await GetResourceAsync<IEnumerable<UserDto>>(BaseUrl);

            result.Response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            result.Data.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetUser_NotExisting_ShouldReturnNotFound()
        {
            var id = GuidHelper.GetGuidFromInt(123);

            var result = await GetResourceAsync<UserDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetUser_Existing_ShouldReturnUser()
        {
            var id = GuidHelper.GetGuidFromInt(1);

            var result = await GetResourceAsync<UserDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
        }

        [Test]
        public async Task CreateUser_NotExisting_ShouldSuccess()
        {
            var now = DateTime.UtcNow;
            var command = new CreateUser
            {
                Email = "  user11@email.com  ",
                Password = "password",
                Username = "  user11  "
            };
            var payload = GetPayload(command);
            
            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().NotBeNullOrWhiteSpace();

            var result = await GetResourceAsync<UserDto>(response.Headers.Location.ToString());

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().NotBe(Guid.Empty);
            result.Data.Email.Should().Be(command.Email.Trim());
            result.Data.Username.Should().Be(command.Username.Trim());
            result.Data.CreatedAt.Should().BeAfter(now);
            result.Data.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        [TestCase("user1@email.com", "bad-password", "user1")]
        [TestCase("user100@email.com", "password", null)]
        [TestCase("user100@email.com", "password", "")]
        [TestCase("user100@email.com", "password", "  ")]
        [TestCase("user100@email.com", null, "user100")]
        [TestCase("user100@email.com", "", "user100")]
        [TestCase("user100@email.com", "  ", "user100")]
        [TestCase(null, "password", "user100")]
        [TestCase("", "password", "user100")]
        [TestCase("  ", "password", "user100")]
        public async Task CreateUser_ExistingUserOrInvalidData_ShouldReturnBadRequest(string email, string password, string username)
        {
            var command = new CreateUser
            {
                Email = email,
                Password = password,
                Username = username
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteUser_Unauthorized_ShouldReturnUnauthorized()
        {
            var id = GuidHelper.GetGuidFromInt(123);

            var response = await Client.DeleteAsync($"{BaseUrl}/{id}");
            
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task DeleteUser_NotExisting_ShouldReturnBadRequest()
        {
            var id = GuidHelper.GetGuidFromInt(123);

            await AddAuthTokenAsync("user1@email.com", "password");
            var response = await Client.DeleteAsync($"{BaseUrl}/{id}");
            RemoveAuthToken();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteUser_Existing_ShouldSuccess()
        {
            var id = GuidHelper.GetGuidFromInt(10);

            await AddAuthTokenAsync("user1@email.com", "password");
            var response = await Client.DeleteAsync($"{BaseUrl}/{id}");
            RemoveAuthToken();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var result = await GetResourceAsync<UserDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
