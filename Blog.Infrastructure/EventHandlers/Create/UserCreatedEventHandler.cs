using Blog.Common.Extensions;
using Blog.Core.Domain;
using Blog.Infrastructure.Events;
using Blog.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Create
{
    public class UserCreatedEventHandler : EntityCreatedEventHandlerBase<User>
    {
        public UserCreatedEventHandler(IMemoryCache cache)
            : base(cache) { }
    }
}
