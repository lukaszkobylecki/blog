using Blog.Infrastructure.Commands.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Post
{
    public class DeletePostHandler : ICommandHandler<DeletePost>
    {
        private readonly IPostService _postService;

        public DeletePostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task HandleAsync(DeletePost command)
        {
            await _postService.DeleteAsync(command.ResourceId);
        }
    }
}
