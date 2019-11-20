using Blog.Infrastructure.Commands;
using Blog.Infrastructure.Commands.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.User
{
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(DeleteUser command)
        {
            await _userService.DeleteAsync(command.ResourceId);
        }
    }
}
