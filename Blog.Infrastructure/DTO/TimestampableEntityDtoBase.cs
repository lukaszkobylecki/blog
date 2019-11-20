using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public abstract class TimestampableEntityDtoBase : EntityDtoBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
