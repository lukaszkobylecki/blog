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

namespace Blog.Api.Controllers
{
    public class PostController : ApiControllerBase
    {
        private readonly IPostService _postService;

        public PostController(ICommandDispatcher commandDispatcher, IPostService postService) 
            : base(commandDispatcher)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.BrowseAsync();

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _postService.GetAsync(id);
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
