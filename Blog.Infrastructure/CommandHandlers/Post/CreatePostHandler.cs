using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Post
{
    public class CreatePostHandler : ICommandHandler<CreatePost>
    {
        private readonly IPostService _postService;

        public CreatePostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task HandleAsync(CreatePost command)
        {
            await _postService.CreateAsync(command.Title.TrimOrEmpty(), command.Content.TrimOrEmpty(),
                command.CategoryId, command.CacheKey);
        }
    }
}
