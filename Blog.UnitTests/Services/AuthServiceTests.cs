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
        private User _nullUser;

        [SetUp]
        public void SetUp()
        {
            _existingUser = new User("user1test.com", "password", "salt", "username");
            _newUser = new User("user2test.com", "password2", "salt2", "username2");
            _nullUser = null;

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAsync(_existingUser.Email)).ReturnsAsync(_existingUser);
            userRepositoryMock.Setup(x => x.GetAsync(_newUser.Email)).ReturnsAsync(_nullUser);

            _authService = new AuthService(userRepositoryMock.Object, MockProvider.Encrypter().Object);
        }

        [Test]
        public async Task login_async_should_throw_error_when_user_with_given_email_does_not_exist()
        {
            await _authService.Invoking(async x => await x.LoginAsync(_newUser.Email, _newUser.Password))
                .Should()
                .ThrowAsync<ServiceException>()
                .ContinueWith(x => x.Result.Which.Code.Should().Be(ErrorCodes.InvalidCredentials));
        }

        [Test]
        public async Task authenticate_async_should_throw_error_when_user_with_given_email_exists_and_password_is_incorrect()
        {
            await _authService.Invoking(async x => await x.LoginAsync(_newUser.Email, "foo"))
                .Should()
                .ThrowAsync<ServiceException>()
                .ContinueWith(x => x.Result.Which.Code.Should().Be(ErrorCodes.InvalidCredentials));
        }

        [Test]
        public async Task authenticate_async_should_success_with_valid_credentials()
        {
            await _authService.Invoking(async x => await x.LoginAsync(_existingUser.Email, _existingUser.Password))
                .Should()
                .NotThrowAsync();
        }
    }
}
