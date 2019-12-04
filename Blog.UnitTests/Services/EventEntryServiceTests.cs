using Blog.Core.Domain;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using Blog.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.Services
{
    [TestFixture]
    public class EventEntryServiceTests
    {
        private IEventEntryService _eventEntryService;
        private Mock<IEventEntryRepository> _eventEntryRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _eventEntryRepositoryMock = new Mock<IEventEntryRepository>();

            _eventEntryService = new EventEntryService(_eventEntryRepositoryMock.Object, MockProvider.AutoMapper());
        }

        [Test]
        public async Task BrowseAsync_ShouldInvokeEventEntryRepositoryBrowseAsync()
        {
            await _eventEntryService.BrowseAsync();

            _eventEntryRepositoryMock.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public void CreateEventEntryAsync_ShouldSuccess()
        {
            _eventEntryService.Invoking(async x => await x.CreateAsync(Guid.NewGuid(), Guid.NewGuid(), "event", "create", "description"))
                .Should()
                .NotThrow();

            _eventEntryRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<EventEntry>()));
        }
    }
}
