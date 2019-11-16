using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.DTO;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Controllers
{
    public class CategoryControllerTests : ControllerTestsBase
    {
        public CategoryControllerTests()
        {
            BaseUrl = "category";
        }

        [Test]
        public async Task GetCategories_ShouldReturnData()
        {
            var result = await GetResource<IEnumerable<CategoryDto>>(BaseUrl);

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetCategory_NotExisting_ShouldReturnNotFound()
        {
            var result = await GetResource<CategoryDto>($"{BaseUrl}/123");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetCategory_Existing_ShouldReturnCategory()
        {
            var id = 1;

            var result = await GetResource<CategoryDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
        }

        [Test]
        public async Task CreateCategory_ValidData_ShouldSuccess()
        {
            var id = 11;
            var now = DateTime.UtcNow;
            var command = new CreateCategory
            {
                Name = "    new-category    "
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().Be($"{BaseUrl}/{id}");

            var result = await GetResource<CategoryDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Id.Should().Be(id);
            result.Data.Name.Should().Be(command.Name.Trim());
            result.Data.UpdatedAt.Should().BeAfter(now);
            result.Data.CreatedAt.Should().BeAfter(now);
        }

        [Test]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase(null)]
        public async Task CreateCategory_InvalidData_ShouldReturnBadRequest(string name)
        {
            var command = new CreateCategory
            {
                Name = name
            };
            var payload = GetPayload(command);

            var response = await Client.PostAsync(BaseUrl, payload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteCategory_NotExisting_ShouldReturnBadRequest()
        {
            var response = await Client.DeleteAsync($"{BaseUrl}/123");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteCategory_Existing_ShouldSuccess()
        {
            var id = 10;

            var response = await Client.DeleteAsync($"{BaseUrl}/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var result = await GetResource<CategoryDto>($"{BaseUrl}/{id}");

            result.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
