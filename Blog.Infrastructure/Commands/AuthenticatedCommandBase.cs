using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public int UserId { get; set; }
    }
}
