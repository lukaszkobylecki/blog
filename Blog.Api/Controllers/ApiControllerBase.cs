using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Command.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        protected Guid UserId => User?.Identity?.IsAuthenticated == true
            ? Guid.Parse(User.Identity.Name)
            : Guid.Empty;

        public ApiControllerBase(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
                authenticatedCommand.CurrentUserId = UserId;

            command.Request = Infrastructure.Command.Commands.Request.Create(Guid.NewGuid(), Request.Host.ToString(), Request.Path, Request.Method);

            await _commandDispatcher.DispatchAsync(command);
        }

        protected async Task<TResult> FetchAsync<TResult>(IQuery<TResult> query)
        {
            return await _queryDispatcher.DispatchAsync(query);
        }
    }
}
