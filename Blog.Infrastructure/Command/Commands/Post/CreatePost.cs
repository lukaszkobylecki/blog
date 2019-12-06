using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands.Post
{
    public class CreatePost : IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
    }
}
