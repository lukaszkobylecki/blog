using Blog.Infrastructure.Commands.Post;
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
    public class PostControllerTests : ControllerTestsBase
    {
        public PostControllerTests()
        {
            BaseUrl = "post";
        }

        [Test]
        public async Task GetPosts_ShouldReturnData()
        {
            var result = await GetResource<IEnumerable<PostDto>>(BaseUrl);

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetPost_NotExisting_ShouldReturnNotFound()
        {
            var result = await GetResource<PostDto>($"{BaseUrl}/123");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetPost_Existing_ShouldReturnNotFound()
        {
            var id = 1;

            var result = await GetResource<PostDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
        }

        [Test]
        public async Task CreatePost_ValidData_ShouldSuccess()
        {
            var id = 11;
            var now = DateTime.UtcNow;
            var command = new CreatePost
            {
                Title = "    title    ",
                Content = "  content   ",
                CategoryId = 1
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().Be($"{BaseUrl}/{id}");

            var result = await GetResource<PostDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
            result.Data.Title.Should().Be(command.Title.Trim());
            result.Data.Content.Should().Be(command.Content.Trim());
            result.Data.CategoryId.Should().Be(command.CategoryId);
            result.Data.CreatedAt.Should().BeAfter(now);
            result.Data.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        [TestCase(null, "content", 1)]
        [TestCase("", "content", 1)]
        [TestCase("   ", "content", 1)]
        [TestCase("title", null, 1)]
        [TestCase("title", "", 1)]
        [TestCase("title", "  ", 1)]
        [TestCase("title", "content", 100)]
        public async Task CreatePost_InvalidData_ShouldReturnBadRequest(string title, string content, int categoryId)
        {
            var command = new CreatePost
            {
                Title = title,
                Content = content,
                CategoryId = categoryId
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeletePost_NotExisting_ShouldReturnBadRequest()
        {
            var response = await Client.DeleteAsync($"{BaseUrl}/123");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeletePost_Existing_ShouldSuccess()
        {
            var id = 10;

            var response = await Client.DeleteAsync($"{BaseUrl}/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var result = await GetResource<PostDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
