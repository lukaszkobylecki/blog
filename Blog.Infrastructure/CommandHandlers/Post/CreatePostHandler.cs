﻿using Blog.Common.Extensions;
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
    public class CreatePostHandler : ICommandHandler<CreatePost>
    {
        private readonly IPostService _postService;
        private readonly IEventPublisher _eventPublisher;

        public CreatePostHandler(IPostService postService, IEventPublisher eventPublisher)
        {
            _postService = postService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(CreatePost command)
        {
            await _postService.CreateAsync(command.ResourceId, command.Title.TrimOrEmpty(), 
                command.Content.TrimOrEmpty(), command.CategoryId);

            var post = await _postService.GetOrFailAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new PostCreated(post));
        }
    }
}
