using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Commands
{
    public interface IResourceCommand : ICommand
    {
        public Guid ResourceId { get; set; }
    }
}
