using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Post
{
    public class CreatePost : CacheableCommandBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
