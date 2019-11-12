using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Users
{
    public class RegisterUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
