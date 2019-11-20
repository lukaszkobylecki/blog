using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; protected set; }
        public int ClusterId { get; set; }
    }
}
