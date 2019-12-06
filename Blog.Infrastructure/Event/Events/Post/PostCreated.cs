using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Event.Events.Post
{
    public class PostCreated : IEvent
    {
        public PostDto Post { get; set; }

        public PostCreated(PostDto post)
        {
            Post = post;
        }
    }
}
