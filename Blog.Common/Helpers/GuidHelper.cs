using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Helpers
{
    public static class GuidHelper
    {
        public static Guid GetGuidFromInt(int value)
            => new Guid(value, 0, 0, new byte[8]);
    }
}
