using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public interface IDateTrackingEntity : IEntity
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
