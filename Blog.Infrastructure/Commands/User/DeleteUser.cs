using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands.User
{
    public class DeleteUser : IAuthenticatedCommand, IResourceCommand
    {
        public Request Request { get; set; }
        public Guid ResourceId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
