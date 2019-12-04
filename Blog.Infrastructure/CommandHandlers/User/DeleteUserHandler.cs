using Blog.Infrastructure.Commands;
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
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserService _userService;
        private readonly IEventPublisher _eventPublisher;

        public DeleteUserHandler(IUserService userService, IEventPublisher eventPublisher)
        {
            _userService = userService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(DeleteUser command)
        {
            await _userService.DeleteAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new UserDeleted(command.ResourceId));
        }
    }
}
