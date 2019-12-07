using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Query.Queries.Post
{
    public class GetPost : IQuery<PostDto>
    {
        public Guid Id { get; set; }
    }
}
