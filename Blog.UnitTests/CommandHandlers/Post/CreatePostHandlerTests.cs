using Blog.Infrastructure.CommandHandlers.Post;
using Blog.Infrastructure.Commands.Post;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Post;
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
    public class CreatePostHandlerTests
    {
        private CreatePostHandler _handler;
        private Mock<IPostService> _postService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _postService = new Mock<IPostService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new CreatePostHandler(_postService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new CreatePost()
            {
                ResourceId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                Content = "content",
                Title = "title"
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _postService.Verify(x => x.CreateAsync(command.ResourceId, command.Title, command.Content, command.CategoryId), Times.Once);
            _postService.Verify(x => x.GetOrFailAsync(command.ResourceId), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<PostCreated>()), Times.Once);
        }
    }
}
