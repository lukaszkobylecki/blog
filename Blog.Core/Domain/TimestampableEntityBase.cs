using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public abstract class TimestampableEntityBase : EntityBase, ITimestampable
    {
        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public TimestampableEntityBase(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            UpdateModificationDate();
        }

        protected void UpdateModificationDate()
            => UpdatedAt = DateTime.UtcNow;
    }
}
