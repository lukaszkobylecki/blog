using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public class EventEntryDto : TimestampableEntityDtoBase
    {
        public string EntityName { get; private set; }
        public Guid EntityId { get; private set; }
        public string Operation { get; private set; }
        public string Description { get; private set; }
    }
}
