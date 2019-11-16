using NUnit.Framework;
using Blog.Core.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Blog.Core.Exceptions;

namespace Blog.UnitTests.Domain
{
    [TestFixture]
    public class UserTests
    {
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = new User("email1@test.com", "password", "salt", "username");
        }
        
        [Test]
        public void Constructor_ShouldSetCreatedAt()
        {
            var now = DateTime.UtcNow;
            var user = new User("emai1@test.com", "password", "salt", "username");

            user.CreatedAt.Should().BeAfter(now);
        }

        [Test]
        public void Constructor_ShouldSetUpdatedAt()
        {
            var now = DateTime.UtcNow;
            var user = new User("emai1@test.com", "password", "salt", "username");

            user.UpdatedAt.Should().BeAfter(now);
        }


        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void SetEmail_InvalidValue_ShouldThrowError(string email)
        {
            _user.Invoking(x => x.SetEmail(email))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEmail);
        }

        [Test]
        public void SetEmail_ValidValue_ShouldSuccess()
        {
            var email = "email2@test.com";
            var lastUpdateTime = _user.UpdatedAt;
            
            _user.SetEmail(email);

            _user.Email.Should().Be(email);
            _user.UpdatedAt.Should().BeAfter(lastUpdateTime);
        }

        [Test]
        public void SetEmail_SameValue_ShouldNotChangeAnything()
        {
            var actualEmail = _user.Email;
            var lastUpdateTime = _user.UpdatedAt;

            _user.SetEmail(actualEmail);

            _user.Email.Should().Be(actualEmail);
            _user.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("     ")]
        public void SetUsername_InvalidValue_ShouldThrowError(string username)
        {
            _user.Invoking(x => x.SetUsername(username))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidUsername);
        }

        [Test]
        public void SetUsername_ValidValue_ShouldSuccess()
        {
            var username = "test";
            var now = DateTime.UtcNow;

            _user.SetUsername(username);

            _user.Username.Should().Be(username);
            _user.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void SetUsername_SameValue_ShouldNotChangeAnything()
        {
            var actualUsername = _user.Username;
            var lastUpdateTime = _user.UpdatedAt;

            _user.SetUsername(_user.Username);

            _user.Username.Should().Be(actualUsername);
            _user.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        [TestCase("", "salt")]
        [TestCase("   ", "salt")]
        [TestCase(null, "salt")]
        [TestCase("password", "")]
        [TestCase("password", "   ")]
        [TestCase("password", null)]
        public void SetPassword_InvalidValues_ShouldThrowError(string password, string salt)
        {
            _user.Invoking(x => x.SetPassword(password, salt))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);
        }

        [Test]
        public void SetPassword_ValidValues_ShouldSuccess()
        {
            var password = "qwerty";
            var salt = "123456";
            var now = DateTime.UtcNow;

            _user.SetPassword(password, salt);

            _user.Password.Should().Be(password);
            _user.Salt.Should().Be(salt);
            _user.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void SetPassword_SameValue_ShouldNotChangeAnything()
        {
            var actualPasword = _user.Password;
            var actualSalt = _user.Salt;
            var lastUpdateTime = _user.UpdatedAt;

            _user.SetPassword(actualPasword, actualSalt);

            _user.Password.Should().Be(actualPasword);
            _user.Salt.Should().Be(actualSalt);
            _user.UpdatedAt.Should().Be(lastUpdateTime);
        }
    }
}
