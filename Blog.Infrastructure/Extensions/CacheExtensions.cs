using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetShort<T>(this IMemoryCache cache, string cacheKey, T value)
            => cache.Set(cacheKey, value, TimeSpan.FromSeconds(10));
    }
}
