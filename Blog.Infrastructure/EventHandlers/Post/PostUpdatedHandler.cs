using Blog.Infrastructure.Events.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Post
{
    public class PostUpdatedHandler : IEventHandler<PostUpdated>
    {
        private readonly IEventEntryService _eventEntryService;

        public PostUpdatedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(PostUpdated @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(PostCreated)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Post.Id,
                "post", "update", $"Title: {@event.Post.Title}");
        }
    }
}
