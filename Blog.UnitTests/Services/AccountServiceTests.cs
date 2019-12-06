using Blog.Common.Helpers;
using Blog.Core.Domain;
using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private IAccountService _accountService;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            var user = new User(GuidHelper.GetGuidFromInt(1), "email", "old", "salt", "username");

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.GetAsync(GuidHelper.GetGuidFromInt(1))).ReturnsAsync(user);

            Mock<IEncrypter> encrypter = new Mock<IEncrypter>();
            encrypter.Setup(x => x.GetHash("old", "salt")).Returns("old");
            encrypter.Setup(x => x.GetSalt("new")).Returns("new-salt");
            encrypter.Setup(x => x.GetHash("new", "new-salt")).Returns("new");

            _accountService = new AccountService(encrypter.Object, _userRepository.Object);
        }

        [Test]
        public void ChangePassword_ExistingUser_ShouldSuccess()
        {
            _accountService.Invoking(async x => await x.ChangePassword(GuidHelper.GetGuidFromInt(1), "old", "new"))
                .Should()
                .NotThrow();

            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void ChangePassword_NotExistingUser_ThrowsError()
        {
            _accountService.Invoking(async x => await x.ChangePassword(GuidHelper.GetGuidFromInt(10), "old", "new"))
                .Should()
                .Throw<ServiceException>()
                .Which.Code.Should().Be(ErrorCodes.UserNotFound);

            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
