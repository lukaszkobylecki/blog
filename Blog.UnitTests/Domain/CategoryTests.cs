using Blog.Core.Domain;
using Blog.Core.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.Domain
{
    [TestFixture]
    public class CategoryTests
    {
        private Category _category;

        [SetUp]
        public void SetUp()
        {
            _category = new Category("category");
        }

        [Test]
        public void Constructor_ShouldSetCreatedAt()
        {
            var now = DateTime.UtcNow;
            var user = new Category("category");

            user.CreatedAt.Should().BeAfter(now);
        }

        [Test]
        public void Constructor_ShouldSetUpdatedAt()
        {
            var now = DateTime.UtcNow;
            var category = new Category("category");

            category.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void SetName_InvalidValue_ShouldThrowError(string name)
        {
            _category.Invoking(x => x.SetName(name))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidCategoryName);
        }

        public void SetName_ValidValue_ShouldSuccess()
        {
            var now = DateTime.Now;
            var name = "test";

            _category.SetName(name);

            _category.Name.Should().Be(name);
            _category.UpdatedAt.Should().BeAfter(now);
        }

        public void SetName_SameValue_ShouldNotChangeAnything()
        {
            var actualName = _category.Name;
            var lastUpdateTime = _category.UpdatedAt;

            _category.SetName(actualName);

            _category.Name.Should().Be(actualName);
            _category.UpdatedAt.Should().Be(lastUpdateTime);
        }
    }
}
