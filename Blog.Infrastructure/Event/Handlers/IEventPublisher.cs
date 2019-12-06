using Blog.Infrastructure.Event.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : IEvent;
    }
}
