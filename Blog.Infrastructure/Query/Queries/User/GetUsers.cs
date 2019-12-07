using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Query.Queries.User
{
    public class GetUsers : IQuery<IEnumerable<UserDto>>
    {
    }
}
