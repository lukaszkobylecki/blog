using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.DTO
{
    public class PostDto : TimestampableEntityDtoBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
