using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Post
{
    public class PostDeleted : IEvent
    {
        public Guid Id { get; set; }

        public PostDeleted(Guid id)
        {
            Id = id;
        }
    }
}
