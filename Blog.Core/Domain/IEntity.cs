using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
        int ClusterId { get; }
    }
}
