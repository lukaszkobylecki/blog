using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        int UserId { get; set; }
    }
}
