using Blog.Common.Extensions;
using Blog.Common.Helpers;
using Blog.Infrastructure.CommandHandlers.Auth;
using Blog.Infrastructure.Commands;
using Blog.Infrastructure.Commands.Auth;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Auth;
using Blog.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.CommandHandlers.Auth
{
    [TestFixture]
    public class LoginHandlerTests
    {
        private LoginHandler _handler;
        private Mock<IAuthService> _authService;
        private Mock<IUserService> _userService;
        private Mock<IJwtHandler> _jwtHandler;
        private Mock<IMemoryCache> _cache;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _authService = new Mock<IAuthService>();
            _userService = new Mock<IUserService>();
            _userService.Setup(x => x.GetOrFailAsync("email")).ReturnsAsync(new UserDto() { Id = GuidHelper.GetGuidFromInt(1), Email = "Email" });
            _jwtHandler = new Mock<IJwtHandler>();
            _jwtHandler.Setup(x => x.CreateToken(GuidHelper.GetGuidFromInt(1))).Returns(new JwtDto() { Expires = 10, Token = "token" });
            _cache = new Mock<IMemoryCache>();
            _cache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(new Mock<ICacheEntry>().Object);
            _eventPublisher = new Mock<IEventPublisher>();

            _handler = new LoginHandler(_authService.Object, _userService.Object, 
                _jwtHandler.Object, _cache.Object, _eventPublisher.Object);
        }

        [Test]
        public void HandleAsync_ShouldInvokeSpecificMethods()
        {
            var command = new Login()
            {
                Email = "  Email   ",
                Password = " Password  ",
                Request = Request.Create(Guid.NewGuid(), "host", "path", "method")
            };

            _handler.Invoking(async x => await x.HandleAsync(command))
                .Should()
                .NotThrow();

            _authService.Verify(x => x.LoginAsync(command.Email.TrimToLower(), command.Password.TrimOrEmpty()), Times.Once);
            _userService.Verify(x => x.GetOrFailAsync(command.Email.TrimToLower()), Times.Once);
            _jwtHandler.Verify(x => x.CreateToken(It.IsAny<Guid>()), Times.Once);
            _cache.Verify(x => x.CreateEntry(command.Request.Id.ToString()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.Is<SignedIn>(x => x.User.Email == command.Email.TrimOrEmpty())), Times.Once);
        }
    }
}
