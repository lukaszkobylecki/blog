using Blog.Infrastructure.Command.Handlers.Category;
using Blog.Infrastructure.Command.Commands.Category;
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events.Category;
using Blog.Infrastructure.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.CommandHandlers.Category
{
    [TestFixture]
    public class CreateCategoryHandlerTests
    {
        private CreateCategoryHandler _handler;
        private Mock<ICategoryService> _categoryService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _categoryService = new Mock<ICategoryService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new CreateCategoryHandler(_categoryService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new CreateCategory
            {
                ResourceId = Guid.NewGuid(),
                Name = "name"
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _categoryService.Verify(x => x.CreateAsync(command.ResourceId, command.Name), Times.Once);
            _categoryService.Verify(x => x.GetOrFailAsync(command.ResourceId), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<CategoryCreated>()), Times.Once);
        }
    }
}
