using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public abstract class AuthenticatedCacheableCommandBase : IAuthenticatedCommand, ICacheableCommand
    {
        public string CacheKey { get; set; }
        public int CurrentUserId { get; set; }
    }
}
