using Blog.Common.Extensions;
using Blog.Core.Domain;
using Blog.Infrastructure.Events;
using Blog.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Create
{
    public abstract class EntityCreatedEventHandlerBase<T> : IEventHandler<EntityCreatedEvent<T>> where T : BaseEntity
    {
        protected readonly IMemoryCache Cache;

        public EntityCreatedEventHandlerBase(IMemoryCache cache)
        {
            Cache = cache;
        }

        public virtual async Task HandleAsync(EntityCreatedEvent<T> @event)
        {
            if (@event.CacheKey.NotEmpty() && @event.Dto != null)
                Cache.SetShort(@event.CacheKey, @event.Dto);

            await Task.CompletedTask;
        }
    }
}
