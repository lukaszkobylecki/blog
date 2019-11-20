using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public abstract class EntityDtoBase
    {
        public Guid Id { get; set; }
    }
}
