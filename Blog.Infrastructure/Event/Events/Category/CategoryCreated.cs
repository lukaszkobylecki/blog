using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Event.Events.Category
{
    public class CategoryCreated : IEvent
    {
        public CategoryDto Category { get; set; }

        public CategoryCreated(CategoryDto category)
        {
            Category = category;
        }
    }
}
