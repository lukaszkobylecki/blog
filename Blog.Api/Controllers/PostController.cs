using System;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Command.Commands.Post;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.Post;
using Blog.Api.Extensions;

namespace Blog.Api.Controllers
{
    public class PostController : ApiControllerBase
    {
        public PostController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) 
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
            => await FetchCollection(new GetPosts());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
            => await FetchSingle(new GetPost().Bind(x => x.Id, id));

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePost command)
            => await CreateAsync(command.Bind(x => x.ResourceId, Guid.NewGuid()), "post");

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePost command)
            => await ExecuteAsync(command.Bind(x => x.ResourceId, id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
            => await ExecuteAsync(new DeletePost().Bind(x => x.ResourceId, id));
    }
}
