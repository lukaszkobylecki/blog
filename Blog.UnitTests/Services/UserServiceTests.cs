using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Blog.Core.Domain;
using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using Blog.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Extensions;

namespace Blog.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IEventPublisher> _eventPublisher;
        private UserService _userService;
        private Mock<User> _existingUser;
        private Mock<User> _newUser;
        private string _cacheKey;

        [SetUp]
        public void SetUp()
        {
            _newUser = new Mock<User>("user2test.com", "password2", "salt2", "username2");
            _newUser.SetupGet(x => x.Id).Returns(0);
            _existingUser = new Mock<User>("user1test.com", "password", "salt", "username");
            _existingUser.SetupGet(x => x.Id).Returns(5);

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Object.Email)).ReturnsAsync(_existingUser.Object);
            _userRepositoryMock.Setup(x => x.GetAsync(_newUser.Object.Email)).ReturnsAsync(() => null);
            _userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Object.Id)).ReturnsAsync(_existingUser.Object);
            _userRepositoryMock.Setup(x => x.GetAsync(_newUser.Object.Id)).ReturnsAsync(() => null);

            _eventPublisher = new Mock<IEventPublisher>();
            _cacheKey = Guid.NewGuid().ToString();

            _userService = new UserService(_userRepositoryMock.Object, MockProvider.AutoMapper(), 
                MockProvider.Encrypter(), _eventPublisher.Object);
        }

        [Test]
        public void CreateAsync_ExistingUser_ShouldThrowError()
        {
            var cacheKey = Guid.NewGuid().ToString();

            _userService.Invoking(async x => await x.CreateAsync(
                    _existingUser.Object.Email, _existingUser.Object.Password,
                    _existingUser.Object.Username, cacheKey))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.EmailInUse);

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityCreatedEvent<User>>()), Times.Never);
        }

        [Test]
        public void CreateAsync_NewUser_ShouldSuccess()
        {
            _userService.Invoking(async x => await x.CreateAsync(_newUser.Object.Email, _newUser.Object.Password, _newUser.Object.Username, _cacheKey))
                .Should()
                .NotThrow();

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityCreatedEvent<User>>()), Times.Once);
        }

        [Test]
        public async Task GetAsyncByEmail_ShouldInvokeUserRepositoryGetAsync()
        {
            var email = MockProvider.RandomString;

            await _userService.GetAsync(email);

            _userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
        }

        [Test]
        public async Task GetAsyncById_ShouldInvokeUserRepositoryGetAsync()
        {
            var id = MockProvider.RandomInt;

            await _userService.GetAsync(id);

            _userRepositoryMock.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public async Task BrowseAsync_ShouldInvokeUserRepositoryBrowseAsync()
        {
            await _userService.BrowseAsync();

            _userRepositoryMock.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public void DeleteAsync_ExistingUser_ShouldSuccess()
        {
            _userService.Invoking(async x => await x.DeleteAsync(_existingUser.Object.Id))
                .Should()
                .NotThrow();

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityDeletedEvent<User>>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingUser_ShouldThrowError()
        {
            _userService.Invoking(async x => await x.DeleteAsync(-1))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.UserNotFound);

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
