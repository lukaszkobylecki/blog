using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid CurrentUserId { get; set; }
    }
}
