using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands
{
    public interface ICommand
    {
        public Request Request { get; set; }
    }
}
