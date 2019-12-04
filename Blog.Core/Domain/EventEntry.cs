using Blog.Common.Extensions;
using Blog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Domain
{
    public class EventEntry : TimestampableEntityBase
    {
        public string EntityName { get; private set; }
        public Guid EntityId { get; private set; }
        public string Operation { get; private set; }
        public string Description { get; private set; }

        protected EventEntry() : base(Guid.NewGuid()) { }

        public EventEntry(Guid id, string entityName, Guid entityId, string operation, string description)
            : base(id)
        {
            SetEntityName(entityName);
            SetEntityId(entityId);
            Operation = operation.TrimToLower();
            Description = description.TrimOrEmpty();
        }

        private void SetEntityName(string entityName)
        {
            if (entityName.Empty())
                throw new DomainException(ErrorCodes.InvalidEntityName, "Entity name can not be empty");

            EntityName = entityName.TrimToLower();
            UpdateModificationDate();
        }

        private void SetEntityId(Guid entityId)
        {
            if (entityId == Guid.Empty)
                throw new DomainException(ErrorCodes.InvalidEntityId, "Entity id can not be empty");

            EntityId = entityId;
            UpdateModificationDate();
        }
    }
}
