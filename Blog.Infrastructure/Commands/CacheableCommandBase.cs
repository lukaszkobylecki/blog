using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public abstract class CacheableCommandBase : ICacheableCommand
    {
        public string CacheKey { get; set; }
    }
}
