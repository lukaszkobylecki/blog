using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public abstract class EntityBase : IEntity
    {
        public virtual int Id { get; protected set; }
    }
}
