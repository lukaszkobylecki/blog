using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Category
{
    public class CategoryDeleted : IEvent
    {
        public Guid Id { get; set; }

        public CategoryDeleted(Guid id)
        {
            Id = id;
        }
    }
}
