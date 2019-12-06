using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Command.Commands
{
    public interface IResourceCommand : ICommand
    {
        public Guid ResourceId { get; set; }
    }
}
