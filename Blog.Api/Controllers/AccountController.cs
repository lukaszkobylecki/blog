﻿using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Command.Commands.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Query.Handlers;

namespace Blog.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
            => await ExecuteAsync(command);
    }
}
