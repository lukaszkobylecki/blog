using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.User
{
    public class CreateUser : IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
