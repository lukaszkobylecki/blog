using Blog.Common.Helpers;
using Blog.Infrastructure.CommandHandlers.User;
using Blog.Infrastructure.Commands.User;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.User;
using Blog.Infrastructure.Services;
using Blog.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.CommandHandlers.User
{
    [TestFixture]
    public class CreateUserHandlerTests
    {
        private CreateUserHandler _handler;
        private Mock<IUserService> _userService;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new CreateUserHandler(_userService.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new CreateUser
            {
                Email = "email",
                Password = "password",
                ResourceId = Guid.NewGuid(),
                Username = "username"
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _userService.Verify(x => x.CreateAsync(command.ResourceId, command.Email, command.Password, command.Username), Times.Once);
            _userService.Verify(x => x.GetOrFailAsync(command.ResourceId), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<UserCreated>()), Times.Once);
        }
    }
}
