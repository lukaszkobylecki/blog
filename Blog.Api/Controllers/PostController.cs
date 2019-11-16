using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.CommandHandlers;
using Blog.Infrastructure.Commands.Post;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Api.Controllers
{
    public class PostController : ApiControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMemoryCache _cache;

        public PostController(ICommandDispatcher commandDispatcher, IPostService postService,
            IMemoryCache cache) 
            : base(commandDispatcher)
        {
            _postService = postService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.BrowseAsync();

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePost command)
        {
            await DispatchAsync(command);

            var post = _cache.Get<PostDto>(command.CacheKey);

            return Created($"post/{post.Id}", post);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var command = new DeletePost { Id = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
