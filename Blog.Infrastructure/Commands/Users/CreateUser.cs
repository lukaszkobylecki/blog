using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Users
{
    public class CreateUser : CacheableCommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
