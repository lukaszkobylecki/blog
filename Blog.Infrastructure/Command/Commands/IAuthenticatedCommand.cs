using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid CurrentUserId { get; set; }
    }
}
