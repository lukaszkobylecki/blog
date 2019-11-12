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
        public void set_created_at_on_constructor()
        {
            var now = DateTime.UtcNow;
            var user = new User("emai1@test.com", "password", "salt", "username");

            user.CreatedAt.Should().BeAfter(now);
        }

        [Test]
        public void set_email_with_null_or_whitespace_string_throws_error()
        {
            _user.Invoking(x => x.SetEmail(""))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEmail);

            _user.Invoking(x => x.SetEmail(null))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEmail);

            _user.Invoking(x => x.SetEmail("    "))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidEmail);
        }

        [Test]
        public void set_email_with_valid_value_should_success()
        {
            var email = "email2@test.com";
            var lastUpdateTime = _user.UpdatedAt;
            _user.SetEmail(email);

            _user.Email.Should().Be(email);
            _user.UpdatedAt.Should().BeAfter(lastUpdateTime);
        }

        [Test]
        public void set_email_with_same_value_should_not_change_anything()
        {
            var actualEmail = _user.Email;
            var lastUpdateTime = _user.UpdatedAt;
            _user.SetEmail(actualEmail);

            _user.Email.Should().Be(actualEmail);
            _user.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        public void set_username_with_null_or_whitespace_string_throws_error()
        {
            _user.Invoking(x => x.SetUsername(""))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidUsername);

            _user.Invoking(x => x.SetUsername(null))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidUsername);

            _user.Invoking(x => x.SetUsername("    "))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidUsername);
        }

        [Test]
        public void set_username_with_valid_value_should_success()
        {
            var username = "username2";
            var now = DateTime.UtcNow;
            _user.SetUsername(username);

            _user.Username.Should().Be(username);
            _user.UpdatedAt.Should().BeAfter(now);
        }

        [Test]
        public void set_username_with_same_value_should_not_change_anything()
        {
            var actualUsername = _user.Username;
            var lastUpdateTime = _user.UpdatedAt;
            _user.SetUsername(_user.Username);

            _user.Username.Should().Be(actualUsername);
            _user.UpdatedAt.Should().Be(lastUpdateTime);
        }

        [Test]
        public void set_password_with_null_or_whitespace_string_throws_error()
        {
            _user.Invoking(x => x.SetPassword("", "x"))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);

            _user.Invoking(x => x.SetPassword(null, "x"))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);

            _user.Invoking(x => x.SetPassword("    ", "x"))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);

            _user.Invoking(x => x.SetPassword("x", ""))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);

            _user.Invoking(x => x.SetPassword("x", null))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);

            _user.Invoking(x => x.SetPassword("x", "    "))
                .Should().Throw<DomainException>()
                .Which.Code.Should().Be(ErrorCodes.InvalidPassword);
        }

        [Test]
        public void set_password_with_valid_values_should_success()
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
        public void set_password_with_same_value_should_not_change_anything()
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
