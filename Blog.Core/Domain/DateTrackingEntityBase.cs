using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public abstract class DateTrackingEntityBase : EntityBase, IDateTrackingEntity
    {
        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public DateTrackingEntityBase()
        {
            CreatedAt = DateTime.UtcNow;
            UpdateModificationDate();
        }

        protected void UpdateModificationDate()
            => UpdatedAt = DateTime.UtcNow;
    }
}
