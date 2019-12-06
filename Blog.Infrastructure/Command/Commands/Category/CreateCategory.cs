using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands.Category
{
    public class CreateCategory : IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
        public string Name { get; set; }
    }
}
