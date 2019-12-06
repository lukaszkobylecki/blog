using Blog.Infrastructure.Event.Events.Account;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers.Account
{
    public class PasswordChangedHandler : IEventHandler<PasswordChanged>
    {
        private readonly IEventEntryService _eventEntryService;

        public PasswordChangedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(PasswordChanged @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(PasswordChanged)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.UserId, "account", "change_password");
        }
    }
}
