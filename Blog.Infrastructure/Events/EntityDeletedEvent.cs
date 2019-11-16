using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events
{
    public class EntityDeletedEvent<T> : IEvent where T : BaseEntity
    {
        public T Entity { get; set; }

        public EntityDeletedEvent(T entity)
        {
            Entity = entity;
        }
    }
}
