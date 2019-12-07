using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Command.Commands.User;
using System;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.User;
using Blog.Api.Extensions;

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
            => await FetchCollection(new GetUsers());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
            => await FetchSingle(new GetUser().Bind(x => x.Id, id));

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
            => await CreateAsync(command.Bind(x => x.ResourceId, Guid.NewGuid()), "user");

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
            => await ExecuteAsync(new DeleteUser().Bind(x => x.ResourceId, id));
    }
}
