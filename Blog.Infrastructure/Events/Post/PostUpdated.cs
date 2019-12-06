using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Events.Post
{
    public class PostUpdated : IEvent
    {
        public PostDto Post { get; set; }

        public PostUpdated(PostDto post)
        {
            Post = post;
        }
    }
}
