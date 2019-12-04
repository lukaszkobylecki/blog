using Blog.Infrastructure.Events.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Post
{
    public class PostDeletedHandler : IEventHandler<PostDeleted>
    {
        private readonly IEventEntryService _eventEntryService;

        public PostDeletedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(PostDeleted @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(PostDeleted)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Id, "post", "delete");
        }
    }
}
