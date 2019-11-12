using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static bool NotEmpty(this string value)
            => !Empty(value);

        public static string OrEmpty(this string value)
            => value.Empty() ? "" : value;

        public static string TrimToLower(this string value)
            => value.OrEmpty().Trim().ToLower();
    }
}
