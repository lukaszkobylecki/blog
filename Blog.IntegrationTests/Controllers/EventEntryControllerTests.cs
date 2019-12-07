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
    [TestFixture]
    public class EventEntryControllerTests : ControllerTestsBase
    {
        public EventEntryControllerTests()
        {
            BaseUrl = "eventEntry";
        }

        [Test]
        public async Task GetEventEntries_ShouldSuccess()
        {
            var result = await GetResourceAsync<IEnumerable<EventEntryDto>>(BaseUrl);

            result.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
