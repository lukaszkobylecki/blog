using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.User
{
    public class DeleteUser : AuthenticatedCommandBase
    {
        public int Id { get; set; }
    }
}
