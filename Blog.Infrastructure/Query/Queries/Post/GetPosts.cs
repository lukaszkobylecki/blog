using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Query.Queries.Post
{
    public class GetPosts : IQuery<IEnumerable<PostDto>>
    {
    }
}
