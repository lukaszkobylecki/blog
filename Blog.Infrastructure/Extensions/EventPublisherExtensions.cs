using Blog.Core.Domain;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Extensions
{
    public static class EventPublisherExtensions
    {
        public static async Task EntityCreated<T>(this IEventPublisher eventPublisher, T entity, object dto, string cacheKey) where T : BaseEntity
            => await eventPublisher.PublishAsync(new EntityCreatedEvent<T>(entity, dto, cacheKey));

        public static async Task EntityUpdated<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
            => await eventPublisher.PublishAsync(new EntityUpdatedEvent<T>(entity));

        public static async Task EntityDeleted<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
            => await eventPublisher.PublishAsync(new EntityDeletedEvent<T>(entity));

    }
}
