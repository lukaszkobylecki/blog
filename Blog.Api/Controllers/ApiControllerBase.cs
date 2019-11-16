using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.CommandHandlers;

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
                authenticatedCommand.CurrentUserId = UserId;
            if (command is ICacheableCommand cacheableCommand)
                cacheableCommand.CacheKey = Guid.NewGuid().ToString();

            await _commandDispatcher.DispatchAsync(command);
        }
    }
}
