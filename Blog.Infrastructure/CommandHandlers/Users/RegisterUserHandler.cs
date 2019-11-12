using Blog.Infrastructure.Commands.Users;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Users
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.RegisterAsync(command.Email.ToLower(), command.Password, command.Username);
        }
    }
}
