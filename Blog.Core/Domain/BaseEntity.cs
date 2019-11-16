using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public abstract class BaseEntity : IEntity
    {
        public virtual int Id { get; protected set; }
    }
}
