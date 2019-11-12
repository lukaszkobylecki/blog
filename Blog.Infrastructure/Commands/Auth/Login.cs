using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Auth
{
    public class Login : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid TokenId { get; set; }
    }
}
