using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands.Auth
{
    public class Login : ICommand
    {
        public Request Request { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
