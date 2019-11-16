using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events
{
    public class EntityCreatedEvent<T> : IEvent where T : BaseEntity
    {
        public T Entity { get; set; }
        public object Dto { get; set; }
        public string CacheKey { get; set; }

        public EntityCreatedEvent(T entity, object dto = null, string cacheKey = null)
        {
            Entity = entity;
            CacheKey = cacheKey;
            Dto = dto;
        }
    }
}
