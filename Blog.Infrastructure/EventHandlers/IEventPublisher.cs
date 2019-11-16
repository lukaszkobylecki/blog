using Blog.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : IEvent;
    }
}
