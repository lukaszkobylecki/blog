using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Category
{
    public class CreateCategory : CacheableCommandBase
    {
        public string Name { get; set; }
    }
}
