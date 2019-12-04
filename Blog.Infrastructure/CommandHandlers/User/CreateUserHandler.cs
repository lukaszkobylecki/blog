using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.User;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.User;
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
        private readonly IEventPublisher _eventPublisher;

        public CreateUserHandler(IUserService userService, IEventPublisher eventPublisher)
        {
            _userService = userService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.CreateAsync(command.ResourceId, command.Email.TrimToLower(), 
                command.Password.TrimOrEmpty(), command.Username.TrimOrEmpty());    

            var user = await _userService.GetOrFailAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new UserCreated(user));  
        }
    }
}
