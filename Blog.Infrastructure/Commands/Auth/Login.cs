using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.Auth
{
    public class Login : CacheableCommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
