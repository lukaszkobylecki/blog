using Blog.Infrastructure.Event.Events.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers.User
{
    public class UserDeletedHandler : IEventHandler<UserDeleted>
    {
        private readonly IEventEntryService _eventEntryService;

        public UserDeletedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(UserDeleted @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(UserDeleted)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Id, "user", "delete");
        }
    }
}
