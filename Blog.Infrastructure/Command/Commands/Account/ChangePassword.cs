using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands.Account
{
    public class ChangePassword : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public Guid CurrentUserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
