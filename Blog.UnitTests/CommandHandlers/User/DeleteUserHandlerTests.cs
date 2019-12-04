using Blog.Infrastructure.CommandHandlers.User;
using Blog.Infrastructure.Commands.User;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.User;
using Blog.Infrastructure.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.CommandHandlers.User
{
    [TestFixture]
    public class DeleteUserHandlerTests
    {
        private DeleteUserHandler _handler;
        private Mock<IUserService> _userService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new DeleteUserHandler(_userService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new DeleteUser()
            {
                ResourceId = Guid.NewGuid()
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _userService.Verify(x => x.DeleteAsync(command.ResourceId), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.Is<UserDeleted>(x => x.Id == command.ResourceId)), Times.Once);
        }
    }
}
