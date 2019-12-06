using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Category
{
    public class UpdateCategory : IResourceCommand
    {
        public Guid ResourceId { get; set; }
        public Request Request { get; set; }
        public string Name { get; set; }
    }
}
