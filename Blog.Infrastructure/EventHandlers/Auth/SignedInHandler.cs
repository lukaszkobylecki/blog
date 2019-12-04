using Blog.Infrastructure.Events.Auth;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Auth
{
    public class SignedInHandler : IEventHandler<SignedIn>
    {
        private readonly IEventEntryService _eventEntryService;

        public SignedInHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(SignedIn @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(SignedIn)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.User.Id, 
                "user", "signIn", $"Username: '{@event.User.Username}' Email: '{@event.User.Email}'");
        }
    }
}
