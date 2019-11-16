using Blog.Core.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.EventHandlers.Create
{
    public class PostCreatedEventHandler : EntityCreatedEventHandlerBase<Post>
    {
        public PostCreatedEventHandler(IMemoryCache cache) 
            : base(cache) { }
    }
}
