using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands.Post
{
    public class DeletePost : IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
    }
}
