using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Category
{
    public class CategoryUpdated : IEvent
    {
        public CategoryDto Category { get; set; }

        public CategoryUpdated(CategoryDto category)
        {
            Category = category;
        }
    }
}
