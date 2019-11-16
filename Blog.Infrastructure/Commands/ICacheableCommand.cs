using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public interface ICacheableCommand : ICommand
    {
        public string CacheKey { get; set; }
    }
}
