using Autofac;
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IComponentContext _context;

        public EventPublisher(IComponentContext context)
        {
            _context = context;
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event: '{typeof(T).Name}' can not be null.");

            _context.TryResolve(out IEventHandler<T> handler);

            if (handler != null) 
                await handler.HandleAsync(@event);
        }
    }
}
