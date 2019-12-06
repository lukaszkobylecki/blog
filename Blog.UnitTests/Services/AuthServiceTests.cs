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
    public class AuthServiceTests
    {
        private IAuthService _authService;
        private User _existingUser;
        private User _newUser;

        [SetUp]
        public void SetUp()
        {
            _existingUser = new User(Guid.NewGuid(), "user1test.com", "password", "salt", "username");
            _newUser = new User(Guid.NewGuid(), "user2test.com", "password2", "salt2", "username2");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Email)).ReturnsAsync(_existingUser);
            userRepositoryMock.Setup(x => x.GetAsync(_newUser.Email)).ReturnsAsync(() => null);

            _authService = new AuthService(userRepositoryMock.Object, MockProvider.Encrypter());
        }

        [Test]
        public void LoginAsync_UserNotExist_ThrowsError()
        {
            _authService.Invoking(async x => await x.LoginAsync(_newUser.Email, _newUser.Password))
                .Should()
                .Throw<ServiceException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidCredentials);
        }

        [Test]
        public void LoginAsync_UserExistsButPasswordIsIncorrect_ThrowsError()
        {
            _authService.Invoking(async x => await x.LoginAsync(_newUser.Email, "foo"))
                .Should()
                .Throw<ServiceException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidCredentials);
        }

        [Test]
        public void LoginAsync_ValidCredentials_ShouldSuccess()
        {
            _authService.Invoking(async x => await x.LoginAsync(_existingUser.Email, _existingUser.Password))
                .Should()
                .NotThrow();
        }
    }
}
