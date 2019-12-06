using Blog.Infrastructure.Command.Handlers.Post;
using Blog.Infrastructure.Command.Commands.Post;
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events.Post;
using Blog.Infrastructure.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.CommandHandlers.Post
{
    [TestFixture]
    public class UpdatePostHandlerTests
    {
        private UpdatePostHandler _handler;
        private Mock<IPostService> _postService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _postService = new Mock<IPostService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new UpdatePostHandler(_postService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new UpdatePost
            {
                ResourceId = Guid.NewGuid(),
                Title = "title",
                Content = "content",
                CategoryId = Guid.NewGuid()
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _postService.Verify(x => x.UpdateAsync(command.ResourceId, command.Title, command.Content, command.CategoryId), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<PostUpdated>()), Times.Once);
        }
    }
}
