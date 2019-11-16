using Blog.Core.Domain;
using Blog.Core.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.Domain
{
    [TestFixture]
    public class PostTests
    {
        private Post _post;

        [SetUp]
        public void SetUp()
        {
            var categoryMock = new Mock<Category>("category");
            categoryMock.SetupGet(x => x.Id).Returns(1);

            _post = new Post("title", "content", categoryMock.Object);
        }

        [Test]
        public void Constructor_ShouldSetDates()
        {
            var now = DateTime.UtcNow;
            var post = new Post("title", "content", new Mock<Category>().Object);

            post.CreatedAt.Should().BeAfter(now);
            post.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void SetTitle_InvalidValue_ShouldThrowError(string content)
        {
            _post.Invoking(x => x.SetTitle(content))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPostTitle);
        }

        [Test]
        public void SetTitle_ValidValue_ShouldSuccess()
        {
            var now = DateTime.UtcNow;
            var title = "test";

            _post.SetTitle(title);

            _post.Title.Should().Be(title);
            _post.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void SetTitle_SameValue_ShouldNotChangeAnything()
        {
            var actualTitle = _post.Title;
            var lastUpdateTime = _post.UpdatedAt;

            _post.SetTitle(actualTitle);

            _post.Title.Should().Be(actualTitle);
            _post.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void SetContent_InvalidValue_ShouldThrowError(string content)
        {
            _post.Invoking(x => x.SetContent(content))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPostContent);
        }

        [Test]
        public void SetContent_ValidValue_ShouldSuccess()
        {
            var now = DateTime.UtcNow;
            var content = "test";

            _post.SetContent(content);

            _post.Content.Should().Be(content);
            _post.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void SetContent_SameValue_ShouldNotChangeAnything()
        {
            var actualContent = _post.Content;
            var lastUpdateTime = _post.UpdatedAt;

            _post.SetContent(actualContent);

            _post.Content.Should().Be(actualContent);
            _post.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        public void SetCategory_InvalidValue_ShouldThrowError()
        {
            _post.Invoking(x => x.SetCategory(null))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPostCategory);
        }

        [Test]
        public void SetCategory_ValidValue_ShouldSuccess()
        {
            var now = DateTime.UtcNow;
            var category = new Mock<Category>().Object;

            _post.SetCategory(category);

            _post.Category.Should().Be(category);
            _post.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void SetCategory_SameValue_ShouldNotChangeAnything()
        {
            var actualCategory = _post.Category;
            var lastUpdateTime = _post.UpdatedAt;

            _post.SetCategory(actualCategory);

            _post.Category.Should().Be(actualCategory);
            _post.UpdatedAt.Should().Be(lastUpdateTime);
        }
    }
}
