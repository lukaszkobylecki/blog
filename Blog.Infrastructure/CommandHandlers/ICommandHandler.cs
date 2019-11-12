using Blog.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
