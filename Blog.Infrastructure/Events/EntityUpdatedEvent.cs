using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events
{
    public class EntityUpdatedEvent<T> : IEvent where T : IEntity
    {
        public T Entity { get; set; }

        public EntityUpdatedEvent(T entity)
        {
            Entity = entity;
        }
    }
}
