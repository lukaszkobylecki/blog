using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public abstract class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public int CurrentUserId { get; set; }
    }
}
