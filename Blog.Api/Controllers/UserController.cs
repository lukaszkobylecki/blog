    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Command.Commands.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.User;

namespace Blog.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await FetchAsync(new GetUsers());

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await FetchAsync(new GetUser { Id = id });
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
        {
            command.ResourceId = Guid.NewGuid();
            await DispatchAsync(command);

            return Created($"user/{command.ResourceId}", null);
        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUser { ResourceId = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
