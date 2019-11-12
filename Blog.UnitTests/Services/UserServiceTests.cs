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

namespace Blog.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;
        private User _existingUser;
        private User _newUser;
        private User _nullUser;
        
        [SetUp]
        public void SetUp()
        {
            _existingUser = new User("user1test.com", "password", "salt", "username");
            _newUser = new User("user2test.com", "password2", "salt2", "username2");
            _nullUser = null;

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Email)).ReturnsAsync(_existingUser);
            _userRepositoryMock.Setup(x => x.GetAsync(_newUser.Email)).ReturnsAsync(_nullUser);
            _userRepositoryMock.Setup(x => x.GetAsync(0)).ReturnsAsync(_existingUser);
            _userRepositoryMock.Setup(x => x.GetAsync(-1)).ReturnsAsync(_nullUser);

            _userService = new UserService(_userRepositoryMock.Object, MockProvider.AutoMapper().Object, MockProvider.Encrypter().Object);
        }

        [Test]
        public async Task register_async_existing_user_should_throw_error()
        {
            await _userService.Invoking(async x => await x.RegisterAsync(_existingUser.Email, _existingUser.Password, _existingUser.Username))
                .Should()
                .ThrowAsync<ServiceException>()
                .ContinueWith(x => x.Result.Which.Code.Should().Be(ErrorCodes.EmailInUse));

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task register_async_new_user_should_success()
        {
            await _userService.Invoking(async x => await x.RegisterAsync(_newUser.Email, _newUser.Password, _newUser.Username))
                .Should()
                .NotThrowAsync();

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task get_async_by_email_should_invoke_user_repository_get_async()
        {
            await _userService.GetAsync(_newUser.Email);
            _userRepositoryMock.Verify(x => x.GetAsync(_newUser.Email), Times.Once);

            await _userService.GetAsync(_existingUser.Email);
            _userRepositoryMock.Verify(x => x.GetAsync(_existingUser.Email), Times.Once);

            await _userService.GetAsync(null);
            _userRepositoryMock.Verify(x => x.GetAsync(null), Times.Once);
        }

        [Test]
        public async Task get_async_by_id_should_invoke_user_repository_get_async()
        {
            await _userService.GetAsync(It.IsAny<int>());
            _userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task browse_async_should_invoke_user_repository_browse_async()
        {
            await _userService.BrowseAsync();
            _userRepositoryMock.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public async Task delete_async_existing_user_should_invoke_user_repository_delete_async()
        {
            await _userService.Invoking(async x => await x.DeleteAsync(_existingUser.Id))
                .Should()
                .NotThrowAsync();

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task delete_async_not_existing_user_should_throw_error()
        {
            await _userService.Invoking(async x => await x.DeleteAsync(-1))
                .Should()
                .ThrowAsync<ServiceException>()
                .ContinueWith(x => x.Result.Which.Code.Should().Be(ErrorCodes.UserNotFound));

            _userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
