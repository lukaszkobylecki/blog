using Blog.Infrastructure.Events.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Post
{
    public class PostCreatedHandler : IEventHandler<PostCreated>
    {
        private readonly IEventEntryService _eventEntryService;

        public PostCreatedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(PostCreated @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(PostCreated)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Post.Id, 
                "post", "create", $"Title: '{@event.Post.Title}'");
        }
    }
}
