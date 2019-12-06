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
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Extensions;
using Blog.Common.Helpers;

namespace Blog.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;
        private Mock<User> _existingUser;
        private Mock<User> _newUser;

        [SetUp]
        public void SetUp()
        {
            _newUser = new Mock<User>(GuidHelper.GetGuidFromInt(0), "user2test.com", "password2", "salt2", "username2");
            _existingUser = new Mock<User>(GuidHelper.GetGuidFromInt(1), "user1test.com", "password", "salt", "username");

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Object.Email)).ReturnsAsync(_existingUser.Object);
            _userRepositoryMock.Setup(x => x.GetAsync(_newUser.Object.Email)).ReturnsAsync(() => null);
            _userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Object.Id)).ReturnsAsync(_existingUser.Object);
            _userRepositoryMock.Setup(x => x.GetAsync(_newUser.Object.Id)).ReturnsAsync(() => null);

            _userService = new UserService(_userRepositoryMock.Object, MockProvider.AutoMapper(), 
                MockProvider.Encrypter());
        }

        [Test]
        public async Task BrowseAsync_ShouldInvokeUserRepositoryBrowseAsync()
        {
            await _userService.BrowseAsync();

            _userRepositoryMock.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public async Task GetAsyncById_ShouldInvokeUserRepositoryGetAsync()
        {
            var id = GuidHelper.GetGuidFromInt(MockProvider.RandomInt);

            await _userService.GetAsync(id);

            _userRepositoryMock.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public async Task GetAsyncByEmail_ShouldInvokeUserRepositoryGetAsync()
        {
            var email = MockProvider.RandomString;

            await _userService.GetAsync(email);

            _userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
        }

        [Test]
        public void CreateAsync_ExistingUser_ShouldThrowError()
        {
            _userService.Invoking(async x => await x.CreateAsync(_existingUser.Object.Id,
                    _existingUser.Object.Email, _existingUser.Object.Password,
                    _existingUser.Object.Username))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.EmailInUse);

            _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public void CreateAsync_NewUser_ShouldSuccess()
        {
            _userService.Invoking(async x => await x.CreateAsync(_newUser.Object.Id, _newUser.Object.Email, _newUser.Object.Password, _newUser.Object.Username))
                .Should()
                .NotThrow();

            _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_ExistingUser_ShouldSuccess()
        {
            _userService.Invoking(async x => await x.DeleteAsync(_existingUser.Object.Id))
                .Should()
                .NotThrow();

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingUser_ShouldThrowError()
        {
            _userService.Invoking(async x => await x.DeleteAsync(GuidHelper.GetGuidFromInt(100)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.UserNotFound);

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
