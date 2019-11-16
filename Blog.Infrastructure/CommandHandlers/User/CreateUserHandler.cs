using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.User
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.CreateAsync(command.Email.TrimToLower(), command.Password.TrimOrEmpty(), command.Username.TrimOrEmpty(), command.CacheKey);
        }
    }
}
