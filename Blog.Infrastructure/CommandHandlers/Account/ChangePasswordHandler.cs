using Blog.Infrastructure.Commands.Account;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Account;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Account
{
    public class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IAccountService _accountService;
        private readonly IEventPublisher _eventPublisher;

        public ChangePasswordHandler(IAccountService accountService, IEventPublisher eventPublisher)
        {
            _accountService = accountService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            await _accountService.ChangePassword(command.CurrentUserId, command.CurrentPassword,
                command.NewPassword);

            await _eventPublisher.PublishAsync(new PasswordChanged(command.CurrentUserId));
        }
    }
}
