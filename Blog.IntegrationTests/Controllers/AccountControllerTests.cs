using Blog.Common.Helpers;
using Blog.Infrastructure.Command.Commands.Account;
using Blog.Infrastructure.DTO;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IntegrationTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests : ControllerTestsBase
    {
        public AccountControllerTests()
        {
            BaseUrl = "account";
        }

        [Test]
        public async Task ChangePassword_ExistingUser_ShouldSuccess()
        {
            var id = GuidHelper.GetGuidFromInt(1);

            var command = new ChangePassword
            {
                CurrentUserId = id,
                CurrentPassword = "password",
                NewPassword = "password2"
            };
            var payload = GetPayload(command);

            await AddAuthTokenAsync("user9@email.com", "password");
            var response = await Client.PostAsync(BaseUrl, payload);
            RemoveAuthToken();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            await AddAuthTokenAsync("user9@email.com", "password2");
            RemoveAuthToken();
        }
    }
}
