using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public class CategoryDto : TimestampableEntityDtoBase
    {
        public string Name { get; set; }
    }
}
