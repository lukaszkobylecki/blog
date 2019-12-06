using Blog.Infrastructure.CommandHandlers.Category;
using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Category;
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
    public class UpdateCategoryHandlerTests
    {
        private UpdateCategoryHandler _handler;
        private Mock<ICategoryService> _categoryService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _categoryService = new Mock<ICategoryService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new UpdateCategoryHandler(_categoryService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new UpdateCategory
            {
                ResourceId = Guid.NewGuid(),
                Name = "new-name"
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _categoryService.Verify(x => x.UpdateAsync(command.ResourceId, command.Name), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<CategoryUpdated>()), Times.Once);
        }
    }
}
