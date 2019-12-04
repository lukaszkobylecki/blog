using Blog.Infrastructure.Commands.Post;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Post;
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
        private readonly IEventPublisher _eventPublisher;

        public DeletePostHandler(IPostService postService, IEventPublisher eventPublisher)
        {
            _postService = postService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(DeletePost command)
        {
            await _postService.DeleteAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new PostDeleted(command.ResourceId));
        }
    }
}
