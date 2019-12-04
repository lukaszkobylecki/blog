using Blog.Common.Helpers;
using Blog.Core.Domain;
using Blog.Core.Exceptions;
using Blog.UnitTests.Mocks;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.Domain
{
    [TestFixture]
    public class EventEntryTests
    {
        [Test]
        public void Constructor_ValidData_ShouldSetDates()
        {
            var now = DateTime.UtcNow;
            var eventEntry = new EventEntry(Guid.NewGuid(), MockProvider.RandomString, Guid.NewGuid(), MockProvider.RandomString, MockProvider.RandomString);

            eventEntry.CreatedAt.Should().BeAfter(now);
            eventEntry.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void Constructor_ValidData_ShouldSetProperties()
        {
            var id = Guid.NewGuid();
            var entityId = Guid.NewGuid();
            var eventEntry = new EventEntry(id, " USER ", entityId, " CREATE ", " DESC ");

            eventEntry.Id.Should().Be(id);
            eventEntry.EntityName.Should().Be("user");
            eventEntry.EntityId.Should().Be(entityId);
            eventEntry.Operation.Should().Be("create");
            eventEntry.Description.Should().Be("DESC");
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void Constructor_InvalidEntityName_ShouldThrowError(string entityName)
        {
            Action constructor = () => 
                new EventEntry(Guid.NewGuid(), entityName, Guid.NewGuid(), MockProvider.RandomString, MockProvider.RandomString);

            constructor.Should()
                .Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEntityName);
        }

        [Test]
        public void Constructor_InvalidEntityId_ShouldThrowError()
        {
            Action constructor = () =>
                new EventEntry(Guid.NewGuid(), MockProvider.RandomString, Guid.Empty, MockProvider.RandomString, MockProvider.RandomString);

            constructor.Should()
                .Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEntityId);
        }
    }
}
