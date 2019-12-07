using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Query.Queries.Category
{
    public class GetCategory : IQuery<CategoryDto>
    {
        public Guid Id { get; set; }
    }
}
