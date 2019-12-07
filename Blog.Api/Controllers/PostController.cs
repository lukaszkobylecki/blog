using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Command.Commands.Post;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.Post;

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
        {
            var posts = await FetchAsync(new GetPosts());

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await FetchAsync(new GetPost { Id = id });
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePost command)
        {
            command.ResourceId = Guid.NewGuid();
            await DispatchAsync(command);

            return Created($"post/{command.ResourceId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePost command)
        {
            command.ResourceId = id;
            await DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var command = new DeletePost { ResourceId = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
