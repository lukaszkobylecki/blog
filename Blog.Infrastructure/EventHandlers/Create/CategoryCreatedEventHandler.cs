using Blog.Common.Extensions;
using Blog.Core.Domain;
using Blog.Infrastructure.Events;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Create
{
    public class CategoryCreatedEventHandler : EntityCreatedEventHandlerBase<Category>
    {
        public CategoryCreatedEventHandler(IMemoryCache cache)
            : base(cache) { }
    }
}
