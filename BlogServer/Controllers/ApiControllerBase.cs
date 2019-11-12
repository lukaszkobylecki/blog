using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Commands;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        protected int UserId => User?.Identity?.IsAuthenticated == true
            ? int.Parse(User.Identity.Name)
            : 0;

        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
                authenticatedCommand.UserId = UserId;

            await _commandDispatcher.DispatchAsync(command);
        }
    }
}
