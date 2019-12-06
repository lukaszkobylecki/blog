using Blog.Infrastructure.Event.Events.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers.User
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly IEventEntryService _eventEntryService;

        public UserCreatedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event: '{typeof(UserCreated)}' can not be null.");
            
            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.User.Id,
                "user", "create", $"Username: '{@event.User.Username}' Email: '{@event.User.Email}'");
        }
    }
}
